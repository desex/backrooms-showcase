using Core;
using Db.GameLevelsSettings.Impls;
using Db.ReviveView;
using Ecs.Utils;
using Game.Models.Camera;
using Game.Services.Analytics;
using Game.Services.Camera;
using Game.Services.Fps;
using Game.Services.General;
using Game.Services.LevelResources;
using Game.Services.SceneLoading;
using Game.Services.Score;
using Game.Services.Start;
using Game.Services.TimeScale;
using Game.UI;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Plugins.SimpleAdWrapper;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

namespace Game.Services.GameState.Impl
{
    public class GameStateService : IGameStateService
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ICameraService _cameraService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IGameLevelsResourcesPaths _gameLevelsResourcesPaths;
        private readonly IGameStartService _gameStartService;
        private readonly ILevelResourcesLoader _levelResourcesLoader;
        private readonly IPlayerCameraHolder _playerCameraHolder;
        private readonly IReviveViewSettings _reviveViewSettings;
        private readonly IAverageFpsService _averageFpsService;
        private readonly IRewardVideoAdService _rewardVideoAdService;
        private readonly ISceneLoadingManager _sceneLoadingManager;
        private readonly IScoreService _scoreService;
        private readonly ISessionService _sessionService;
        private readonly SignalBus _signalBus;
        private readonly ITimeScaleService _timeScaleService;

        public GameStateService(
            GameContext game,
            SignalBus signalBus,
            IScoreService scoreService,
            IPlayerCameraHolder playerCameraHolder,
            IGameStartService gameStartService,
            ISceneLoadingManager sceneLoadingManager,
            ICommandBuffer commandBuffer,
            IAnalyticsService analyticsService,
            ISessionService sessionService,
            ILevelResourcesLoader levelResourcesLoader,
            ITimeScaleService timeScaleService,
            IGameLevelsResourcesPaths gameLevelsResourcesPaths,
            IRewardVideoAdService rewardVideoAdService,
            IReviveViewSettings reviveViewSettings,
            IAverageFpsService averageFpsService
        )
        {
            _game = game;
            _signalBus = signalBus;
            _scoreService = scoreService;
            _playerCameraHolder = playerCameraHolder;
            _gameStartService = gameStartService;
            _sceneLoadingManager = sceneLoadingManager;
            _commandBuffer = commandBuffer;
            _analyticsService = analyticsService;
            _sessionService = sessionService;
            _levelResourcesLoader = levelResourcesLoader;
            _gameLevelsResourcesPaths = gameLevelsResourcesPaths;
            _timeScaleService = timeScaleService;
            _rewardVideoAdService = rewardVideoAdService;
            _reviveViewSettings = reviveViewSettings;
            _averageFpsService = averageFpsService;
        }

        public void EnterMainMenu()
        {
            _game.ReplaceGameState(EGameState.MainMenu);

            _playerCameraHolder.SwitchToMainMenuCamera();
            _signalBus.OpenWindow<MainMenuWindow>();
        }

        public void EnterGameplay()
        {
            _signalBus.OpenWindow<EmptyWindow>();

            if (_game.GameStateEntity.GameState.Value == EGameState.Game)
                return;

            _game.PauseEntity.ReplacePause(false);
            _game.ReplaceGameState(EGameState.Game);
            _averageFpsService.Start().ForgetAndWrapExceptions();
            _commandBuffer.ResetSpeedShader();
            _playerCameraHolder.SwitchToGamePlayCamera();
            _analyticsService.HandleLevelStart();
        }

        public void EnterTutorial()
        {
            _game.PauseEntity.ReplacePause(false);
            _game.ReplaceGameState(EGameState.Game);

            _commandBuffer.ResetSpeedShader();
            _playerCameraHolder.SwitchToGamePlayCamera();
            _signalBus.OpenWindow<TutorialWindow>();

            _analyticsService.HandleLevelStart();
        }

        public void EnterLose()
        {
            _game.PauseEntity.ReplacePause(false);
            _analyticsService.HandleLevelFinish();
            _game.ReplaceGameState(EGameState.Lose);
            _signalBus.OpenWindow<DeathWindow>();
            _scoreService.UpdateBestResults();
        }

        public void Restart(bool needSkipMainMenu)
        {
            _levelResourcesLoader.Load(_gameLevelsResourcesPaths.StartGameLevel);
            _gameStartService.SetSkipMainMenu(needSkipMainMenu);
            _sessionService.IncreaseRestartCount();
            _analyticsService.HandleLevelRestart();

            RenderSettings.fog = true;

            _timeScaleService.ResetTimeScaleInstantly();

            if (needSkipMainMenu)
                _sceneLoadingManager.ReloadGameWithMainMenuSkip();
            else
                _sceneLoadingManager.ReloadGame();
        }

        public void EnterRevivingChoiceWithDelay()
        {
            _game.ReplaceGameState(EGameState.RevivingChoice);

            DelayedInvocationService.InvokeIgnoreTimeScale(EnterChoice, _reviveViewSettings.DelayFromSeconds);
        }

        public void EnterRevivingChoice()
        {
            _game.ReplaceGameState(EGameState.RevivingChoice);

            EnterChoice();
        }

        public void Revive()
        {
            var playerUid = _game.PlayerEntity.Uid.Value;
            _analyticsService.HandleRevive();
            _commandBuffer.Revive(playerUid);
            _signalBus.BackWindow();
            _game.ReplaceGameState(EGameState.Game);
        }

        private void EnterChoice()
        {
            _signalBus.OpenWindow<ReviveWindow>();
            _game.PauseEntity.ReplacePause(true);
        }
    }
}