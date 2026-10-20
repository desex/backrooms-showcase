using System;
using System.Threading;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Cysharp.Threading.Tasks;
using Plugins.SimpleAdWrapper;
using Plugins.SimpleAdWrapper.Wrappers;
using R3;
using UnityEngine;
using Zenject;

namespace Libs.SimpleAppodealWrapper.Wrappers
{
    public class AppodealRewardVideoAdWrapper : 
        IInitializable,
        IRewardVideoAdWrapper,
        IRewardedVideoAdListener,
        IDisposable
    {
        private readonly ReactiveCommand<AdStateUpdate> _onStateCommand = new();

        private readonly TimeSpan _loadTimeout;
        
        private UniTaskCompletionSource _loadCompletionSource = new();

        private InternalState _state;

        public string AdDealerName => "Appodeal";

        public Observable<AdStateUpdate> OnState => _onStateCommand;

        public bool IsAdsForbidden => false;

        public AppodealRewardVideoAdWrapper(AppodealSettings settings)
        {
            _loadTimeout = TimeSpan.FromSeconds(settings.RewardVideoLoadTimeSec);
        }

        public void Initialize()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Initialize");

            Appodeal.SetRewardedVideoCallbacks(this);
        }
        
        public bool IsLoaded() => Appodeal.IsLoaded(AppodealAdType.RewardedVideo);

        public async UniTask<bool> WaitLoaded()
        {
            if (_loadCompletionSource.Task.Status != UniTaskStatus.Succeeded)
            {
                await _loadCompletionSource.Task;
            }

            return IsLoaded();
        }

        public void Dispose()
        {
            _onStateCommand.Dispose();
            _state.ShowCompletionSource?.TrySetCanceled();
            Appodeal.SetRewardedVideoCallbacks(null);
        }

        private void Reset()
        {
            _state = new();
        }

        public async UniTask<bool> TryPreloadAsync()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UpdateStateError(EAdState.None, "No internet connection");
                return false;
            }
            
            if (IsLoaded())
            {
                UpdateState(EAdState.Loaded);
                return true;
            }
        
            if (_loadCompletionSource != default && _loadCompletionSource.Task.Status != UniTaskStatus.Succeeded)
            {
                await _loadCompletionSource.Task;
                return IsLoaded();
            }
        
            using var cancellationTokenSource = new CancellationTokenSource();
        
            _loadCompletionSource = new();
        
            UpdateState(EAdState.Load);
            
            Appodeal.Cache(AppodealAdType.RewardedVideo);
        
            UniTask.Delay(_loadTimeout, cancellationToken: cancellationTokenSource.Token)
                   .ContinueWith(() => _loadCompletionSource?.TrySetResult());
        
            await _loadCompletionSource.Task;
        
            cancellationTokenSource.Cancel();
        
            var isLoaded = IsLoaded();
        
            if (!isLoaded)
            {
                UpdateStateError(EAdState.FailToLoad, "Ad load failed");
                return false;
            }
        
            UpdateState(EAdState.Loaded);
        
            return true;
        }

        public async UniTask<ShowAdResult> ShowAsync(string place)
        {
            if (_state.ShowCompletionSource != default)
                throw new("Already in progress");

            _state.Place = place;
            _state.StartTime = DateTimeOffset.Now;

            try
            {
                UpdateState(EAdState.Preload);

                _state.ShowCompletionSource = new();

                if (Application.internetReachability == NetworkReachability.NotReachable)
                    throw new("No internet connection");
                
                var adLoaded = await TryPreloadAsync();
                if (!adLoaded)
                {
                    _state.WasFail = true;
                    return GetResult();
                }

                if (string.IsNullOrWhiteSpace(place))
                    Appodeal.Show(AppodealShowStyle.RewardedVideo);
                else
                    Appodeal.Show(AppodealShowStyle.RewardedVideo, place);

                await _state.ShowCompletionSource.Task;

                return GetResult();
            }
            catch (Exception e)
            {
                _state.WasFail = true;
                UpdateStateError(EAdState.Fail, e.Message);

                Debug.Log(e);

                return GetResult();
            }
            finally
            {
                UpdateState(EAdState.Complete);
                Reset();
            }
        }

        private ShowAdResult GetResult() => new()
        {
            AdType = EAdType.Video,
            AdDealer = AdDealerName,
            WasClicked = _state.WasClicked,
            WasShown = _state.WasShown,
            WasFailed = _state.WasFail,
            WasSkipped = _state.WasSkipped
        };

        private void UpdateState(EAdState state)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Video,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = null,
                }
            );
        }

        private void UpdateStateError(EAdState state, string error)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Video,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = error,
                }
            );
        }
        
        private struct InternalState
        {
            public UniTaskCompletionSource ShowCompletionSource;
            public bool WasClicked;
            public bool WasShown;
            public bool WasFail;
            public bool WasSkipped;
            public string Place;
            public DateTimeOffset StartTime;
        }
        
        #region CALLBACKS

        public void OnRewardedVideoLoaded(bool isPrecache)
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video loaded. Precache: {isPrecache}");

            UpdateState(EAdState.Loaded);
            _loadCompletionSource?.TrySetResult();
        }

        public void OnRewardedVideoFailedToLoad()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video failed to load");

            _loadCompletionSource?.TrySetResult();
        }

        public void OnRewardedVideoShowFailed()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video show failed");

            UpdateStateError(EAdState.FailToPlay, "Ad show failed");

            _state.WasFail = true;
            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnRewardedVideoShown()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video shown");

            UpdateState(EAdState.Show);

            _state.WasShown = true;
        }

        public void OnRewardedVideoFinished(double amount, string currency)
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video finished, amount: {amount}, currency: {currency}");

            UpdateState(EAdState.Finished);
        }

        public void OnRewardedVideoClosed(bool finished)
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video closed. Finished: {finished}");

            UpdateState(EAdState.Close);

            _state.WasSkipped = !finished;

            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnRewardedVideoExpired()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video expired");

            UpdateStateError(EAdState.Expire, "Ad expired");

            _state.WasFail = true;
            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnRewardedVideoClicked()
        {
            Debug.Log($"[{nameof(AppodealRewardVideoAdWrapper)}] Reward video clicked");

            UpdateState(EAdState.Clicked);

            _state.WasClicked = true;
        }

        #endregion
    }
}