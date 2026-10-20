using System;
using Cysharp.Threading.Tasks;
using Game.Utils.UniTaskExtensions;
using PdUtils.SceneLoadingProcessor.Impls;
using Plugins.SimpleAdWrapper;

namespace Game.Services.SceneLoading.Processors
{
    public class PreloadAds : Process
    {
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly IRewardVideoAdService _rewardVideoAdService;

        public PreloadAds(IRewardVideoAdService rewardVideoAdService, IInterstitialAdService interstitialAdService)
        {
            _rewardVideoAdService = rewardVideoAdService;
            _interstitialAdService = interstitialAdService;
        }

        public override async void Do(Action onComplete)
        {
            _rewardVideoAdService.TryPreloadAsync()
                .ForgetAndWrapExceptions();

            _interstitialAdService.TryPreloadAsync()
                .ForgetAndWrapExceptions();

            await UniTask.Yield();
            onComplete();
        }
    }
}