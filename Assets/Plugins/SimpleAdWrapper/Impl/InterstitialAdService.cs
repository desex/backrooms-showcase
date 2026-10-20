using System;
using Cysharp.Threading.Tasks;
using Plugins.SimpleAdWrapper.Wrappers;
using R3;
using UnityEngine;
using Zenject;

namespace Plugins.SimpleAdWrapper.Impl
{
    public class InterstitialAdService : IInterstitialAdService, IInitializable, IDisposable
    {
        private readonly ReactiveCommand<AdStateUpdate> _onStateCommand = new();
        private readonly IInterstitialAdWrapper _wrapper;

        private IDisposable _disposable = Disposable.Empty;

        public Observable<AdStateUpdate> OnState => _onStateCommand;
        
        public InterstitialAdService(IInterstitialAdWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public void Initialize()
        {
            Debug.Log($"[{nameof(InterstitialAdService)}] Initialize interstitial ad dealer: {_wrapper.AdDealerName}");
            _disposable = _wrapper.OnState.Subscribe(state => _onStateCommand.Execute(state));
        }

        public bool IsLoaded() => _wrapper.IsLoaded();
        public UniTask<bool> TryPreloadAsync() => _wrapper.TryPreloadAsync();
        public UniTask<bool> WaitLoaded() => _wrapper.WaitLoaded();
        
        public async UniTask<ShowAdResult> ShowAdAsync(string place)
        {
            if (!_wrapper.IsAdsForbidden)
                return await _wrapper.ShowAsync(place);
            
            Debug.LogError(
                $"[{nameof(InterstitialAdService)}] Ad forbidden failed. Ad dealer: {_wrapper.AdDealerName}"
            );

            _onStateCommand.Execute(new()
                {
                    AdType = EAdType.Interstitial,
                    AdDealer = _wrapper.AdDealerName,
                    State = EAdState.Forbidden,
                    Place = place,
                    Duration = 0,
                    Error = "Ad forbidden",
                }
            );

            return new()
            {
                AdType = EAdType.Interstitial,
                AdDealer = _wrapper.AdDealerName,
                WasFailed = true,
            };
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _onStateCommand?.Dispose();
        }
    }
}