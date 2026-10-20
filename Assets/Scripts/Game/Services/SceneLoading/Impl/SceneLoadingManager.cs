using Db.SceneLoading;
using Db.Shaders;
using Game.Services.Render;
using Game.Services.SceneLoading.Game.SceneLoading;
using Game.Services.SceneLoading.Processors;
using Game.Utils;
using PdUtils.SceneLoadingProcessor.Impls;
using Plugins.SimpleAdWrapper;
using Project.Ui.Loading.Controllers;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Services.SceneLoading.Impl
{
    public class SceneLoadingManager : ISceneLoadingManager
    {
        private const EGameLevelType START_SCENE = EGameLevelType.Yellow;
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly IReloadingController _reloadingController;
        private readonly IRewardVideoAdService _rewardVideoAdService;
        private readonly IScenesBase _scenesBase;
        private readonly IWarmupShadersBase _warmupShadersBase;
        private readonly IGlitchService _glitchService;
        private readonly IWhiteNoiseService _whiteNoiseService;

        private readonly SignalBus _signalBus;

        private LoadingProcessor _processor;

        private string StartSceneName => _scenesBase.GetSceneNameByType(START_SCENE);

        public SceneLoadingManager(
            SignalBus signalBus,
            IScenesBase scenesBase,
            IReloadingController reloadingController,
            IRewardVideoAdService rewardVideoAdService,
            IInterstitialAdService interstitialAdService,
            IWarmupShadersBase warmupShadersBase,
            IGlitchService glitchService,
            IWhiteNoiseService whiteNoiseService
        )
        {
            _signalBus = signalBus;
            _scenesBase = scenesBase;
            _reloadingController = reloadingController;
            _rewardVideoAdService = rewardVideoAdService;
            _interstitialAdService = interstitialAdService;
            _warmupShadersBase = warmupShadersBase;
            _glitchService = glitchService;
            _whiteNoiseService = whiteNoiseService;
        }

        public void LoadGameFromSplash()
        {
            _processor = new LoadingProcessor();

            _processor.AddProcess(new ShowWhiteNoiseProcess(_glitchService, _whiteNoiseService))
                .AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new WarmupShadersProcess(_warmupShadersBase))
                .AddProcess(new LoadingProcess(StartSceneName, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(StartSceneName))
                .AddProcess(new ShowGlitchProcess(_glitchService, _whiteNoiseService))
                .AddProcess(new UnloadProcess(SceneNames.SPLASH))
                .AddProcess(new RunContextProcess(nameof(GameContext)))
                .AddProcess(new PreloadAds(_rewardVideoAdService, _interstitialAdService))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void ReloadGame()
        {
            _processor = new LoadingProcessor();

            _processor.AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(StartSceneName, LoadSceneMode.Single))
                .AddProcess(new SetActiveSceneProcess(StartSceneName))
                .AddProcess(new RunContextProcess(nameof(GameContext)))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void ReloadGameWithMainMenuSkip()
        {
            _processor = new LoadingProcessor();

            _processor.AddProcess(new ReloadLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(StartSceneName, LoadSceneMode.Single))
                .AddProcess(new SetActiveSceneProcess(StartSceneName))
                .AddProcess(new RunContextProcess(nameof(GameContext)))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new SmoothlyCloseReloadWindow(_reloadingController))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void TransitTo(string sceneName)
        {
            _processor = new LoadingProcessor();

            _processor.AddProcess(new ReloadLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(sceneName, LoadSceneMode.Single))
                .AddProcess(new SetActiveSceneProcess(sceneName))
                .AddProcess(new RunContextProcess(nameof(GameContext)))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new SmoothlyCloseReloadWindow(_reloadingController))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public float GetProgress()
        {
            return _processor?.Progress ?? 0f;
        }
    }
}