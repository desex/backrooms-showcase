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
    public class AppodealInterstitialAdWrapper : 
        IInitializable,
        IInterstitialAdWrapper,
        IInterstitialAdListener,
        IDisposable
    {
        private readonly ReactiveCommand<AdStateUpdate> _onStateCommand = new();

        private readonly TimeSpan _loadTimeout;
        
        private UniTaskCompletionSource _loadCompletionSource = new();

        private InternalState _state;

        public string AdDealerName => "Appodeal";

        public Observable<AdStateUpdate> OnState => _onStateCommand;

        public bool IsAdsForbidden => false;

        public AppodealInterstitialAdWrapper(AppodealSettings settings)
        {
            _loadTimeout = TimeSpan.FromSeconds(settings.InterstitialLoadTimeSec);
        }

        public void Initialize()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Initialize");
            Appodeal.SetInterstitialCallbacks(this);
        }
        
        public bool IsLoaded() => Appodeal.IsLoaded(AppodealAdType.Interstitial);

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
            Appodeal.SetInterstitialCallbacks(null);
        }

        private void Reset()
        {
            _state = new();
        }
        
        public async UniTask<bool> TryPreloadAsync()
        {
            if (IsLoaded())
                return true;
        
            if (_loadCompletionSource != default && _loadCompletionSource.Task.Status != UniTaskStatus.Succeeded)
            {
                await _loadCompletionSource.Task;
                return IsLoaded();
            }
        
            using var cancellationTokenSource = new CancellationTokenSource();
        
            _loadCompletionSource = new();
        
            UpdateState(EAdState.Load);

            // blocking call?
            Appodeal.Cache(AppodealAdType.Interstitial);
        
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

                var adLoaded = await TryPreloadAsync();
                if (!adLoaded)
                {
                    _state.WasFail = true;
                    return GetResult();
                }

                if (string.IsNullOrWhiteSpace(place))
                    Appodeal.Show(AppodealShowStyle.Interstitial);
                else
                    Appodeal.Show(AppodealShowStyle.Interstitial, place);

                await _state.ShowCompletionSource.Task;

                return GetResult();
            }
            catch (Exception e)
            {
                _state.WasFail = true;
                UpdateStateError(EAdState.Fail, e.Message);

                Debug.LogException(e);

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

        public void UpdateState(EAdState state)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Interstitial,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = null
                }
            );
        }

        public void UpdateStateError(EAdState state, string error)
        {
            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Interstitial,
                    AdDealer = AdDealerName,
                    State = state,
                    Place = _state.Place,
                    Duration = (DateTimeOffset.Now - _state.StartTime).TotalSeconds,
                    Error = error
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
        
        public void OnInterstitialLoaded(bool isPrecache)
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial loaded. Precache: {isPrecache}");

            _loadCompletionSource?.TrySetResult();
        }

        public void OnInterstitialFailedToLoad()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial failed to load");

            _loadCompletionSource?.TrySetResult();
        }

        public void OnInterstitialShowFailed()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial show failed");

            UpdateStateError(EAdState.FailToPlay, "Ad show failed");

            _state.WasFail = true;
            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnInterstitialShown()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial shown");

            UpdateState(EAdState.Show);

            _state.WasShown = true;
        }

        public void OnInterstitialClosed()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial closed");

            UpdateState(EAdState.Close);
            
            _state.WasSkipped = false;

            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnInterstitialExpired()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial expired");

            UpdateStateError(EAdState.Expire, "Ad expired");

            _state.WasFail = true;
            _state.ShowCompletionSource?.TrySetResult();
        }

        public void OnInterstitialClicked()
        {
            Debug.Log($"[{nameof(AppodealInterstitialAdWrapper)}] Interstitial clicked");

            UpdateState(EAdState.Clicked);

            _state.WasClicked = true;
        }
        
        #endregion
    }
}