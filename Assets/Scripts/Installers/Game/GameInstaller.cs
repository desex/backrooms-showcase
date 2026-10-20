using Core.Utils;
using Ecs.Utils.Chunks.Impl;
using Ecs.Utils.Impl;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Models.Camera.Impl;
using Game.Models.Input;
using Game.Services;
using Game.Services.Ai.Impl;
using Game.Services.Analytics.Impl;
using Game.Services.Buffer.Impl;
using Game.Services.Camera.Impl;
using Game.Services.Chaser;
using Game.Services.Chaser.Impl;
using Game.Services.CheckPoints.Impl;
using Game.Services.Chunks.Impl;
using Game.Services.CollectablesItem.Impl;
using Game.Services.ComboWords.Impl;
using Game.Services.CurvedWorld.Impl;
using Game.Services.DistanceCalculation.Impl;
using Game.Services.Effects.Impl;
using Game.Services.Fps.Impl;
using Game.Services.GameState.Impl;
using Game.Services.Generation.Impl;
using Game.Services.Ghosts.Sound.Impl;
using Game.Services.Hint.Impl;
using Game.Services.Level.Impl;
using Game.Services.Lights.Impl;
using Game.Services.Lights.Impl.Commands.Impl.Parts;
using Game.Services.Lights.Impl.Handlers;
using Game.Services.Lights.Impl.Handlers.Impl;
using Game.Services.Obstacles.Impls;
using Game.Services.Performance.Impl;
using Game.Services.Player.Impl;
using Game.Services.Pool.Impls;
using Game.Services.PoolCleaner.Impl;
using Game.Services.QTE.Impl;
using Game.Services.Random.Impl;
using Game.Services.Render.Impl;
using Game.Services.Revive.Impl;
using Game.Services.Score.Impl;
using Game.Services.Settings.Impl;
using Game.Services.Sound.Chaser.Impl;
using Game.Services.Sound.Impl;
using Game.Services.TimeScale.Impl;
using Game.Services.Tutorial.Impl;
using Game.Services.TutorialStateMachine.Impl;
using Game.Services.UiVisibility.Impl;
using Game.Signals;
using Game.UI;
using Game.UI.SettingsPopup;
using Game.Utils.Chunks.Pool.Impl;
using Game.Utils.ColliderFilters;
using Game.Utils.ColliderFilters.Impl;
using Game.Utils.Collisions.Impl;
using Game.Utils.DistanceCalculation;
using Game.Utils.Linecasts;
using Game.Utils.Linecasts.Impl;
using Game.Utils.Overlaps.Impl;
using Game.Utils.TimerWithTicks;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Game.Services.PlayerInput;
using Game.Services.DebugService.Impl;
#endif

//Do not remove. For defines
#if TEST_ANALYTICS && UNITY_EDITOR
using Libs.AnalyticsAppMetrica;
#elif !UNITY_EDITOR
using Libs.AnalyticsAppMetrica;
using Libs.AppodealWrapper.Analytics;
using Libs.AppsFlyerAttribution;
#endif
//Do not remove. For defines

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerBoxTracker _playerBoxTracker;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            Bind();
            BindPools();
            BindServices();
            BindWindows();
            BindManagers();
        }
        
        private void DeclareSignals()
        {
            Container.DeclareSignal<SignalLevelStart>();
	        Container.DeclareSignal<SignalLevelRestart>();
	        Container.DeclareSignal<SignalLevelFinish>();
	        Container.DeclareSignal<SignalPlayerDeath>();
	        Container.DeclareSignal<SignalTutorialStepFinish>();
	        Container.DeclareSignal<SignalTutorialStepStart>();
	        Container.DeclareSignal<SignalRevive>();
            Container.DeclareSignal<SignalVideoAdsTriggered>();
            Container.DeclareSignal<SignalVideoAdsStarted>();
            Container.DeclareSignal<SignalVideoAdsComplete>();
            Container.DeclareSignal<SignalTutorialFinish>();
            Container.DeclareSignal<SignalLevelsTransitions>();
            Container.DeclareSignal<SignalFpsDecrease>();
        }

        private void Bind()
        {
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialAnalyticsService>().AsSingle();
	        Container.BindInterfacesAndSelfTo<LevelSessionService>().AsSingle();
	        
#if TEST_ANALYTICS && UNITY_EDITOR
	        AppMetricaServiceInstaller.Install(Container);
#elif !UNITY_EDITOR
	        AppMetricaServiceInstaller.Install(Container);
	        AppodealAnalyticsServiceInstaller.Install(Container);
	        AppsFlyerServiceInstaller.Install(Container);
#endif
            Container.Bind<LateFixedManager>().AsSingle();
            Container.Bind<LateFixedUpdate>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerCameraHolder>().AsSingle();

            Container.BindInterfacesAndSelfTo<GenerationTasksHandler>().AsSingle();

            Container.Bind<IPlayerBoxTracker>()
                .To<PlayerBoxTracker>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }

        private void BindPools()
        {
            Container.BindInterfacesAndSelfTo<ChunkPool>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<WallPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialWallPool>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ObstaclePool>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialObstaclePool>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectablesItemPool>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AnalyticSignalTypeGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdaptivePerformanceService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationFactorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialObstacleService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PoolEffectsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnderWaterRendererService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WallService>().AsSingle();
            Container.BindInterfacesAndSelfTo<QteService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChunkService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialObstacleGenerationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleGenerationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleSpawnValidatorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WallsGenerationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialWallsGenerationService>().AsSingle();
            Container.BindInterfacesTo<PrefabPoolService>().AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindFromSubstitute<IChaserPresentationService, ChaserPresentationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementTypeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpeedEffectService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UiSpeedService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerSpeedService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TravelatorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerTiltingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerAnimationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSlideService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSizeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ComboWordsService>().AsSingle();
	        Container.BindInterfacesAndSelfTo<CollectiblesStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<SequentialCollectableSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurvedWorldService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChunkCurvedService>().AsSingle();
            Container.BindInterfacesTo<ReviveObstacleService>().AsSingle();
            Container.BindInterfacesTo<CollisionService>().AsSingle();
            Container.BindInterfacesTo<GhostSoundDispatcher>().AsSingle();
            Container.BindInterfacesTo<DistanceCalculationService>().AsSingle();
            Container.BindInterfacesTo<ChunkSegmentService>().AsSingle();
            Container.BindInterfacesTo<ChunkSegmentPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClearablePoolsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeScaleService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SlowingDownReviveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiVisibilityService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChaserSoundZoneService>().AsSingle();
            Container.Bind<SequentialChunkGenerationService>().AsSingle();
            Container.BindInterfacesTo<SequentialChunkPredictionService>().AsSingle();
            Container.Bind<TutorialChunkGenerationService>().AsSingle();
            Container.Bind<RandomChunkGenerationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TextLoggerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BufferSwipeService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TutorialStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialHintService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialSequencesProvider>().AsSingle();
            Container.BindInterfacesTo<AverageFpsService>().AsSingle();

            Container.BindInterfacesTo<LevelService>().AsSingle();
	       
            Container.BindInterfacesAndSelfTo<CollectablesSpawnService>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();

            Container.Bind<TimerWithTicks>().AsTransient().WhenInjectedInto<ScoreService>();
            
            Container.BindInterfacesAndSelfTo<ChunkGenerationServiceProvider>().AsSingle();
            Container.Bind<IColliderFilterService>().To<ColliderFilterService>().AsSingle();
            Container.Bind<ILinecastProvider>().To<LinecastProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<OverlapService>().AsSingle();

            BindLightService();

            Container.BindInterfacesAndSelfTo<SettingsService>().AsSingle();
            Container.BindInterfacesTo<SoundService>().AsSingle().Lazy();
            
            Container.BindInterfacesTo<BehaviourTreesService>().AsSingle().Lazy();
            Container.BindInterfacesTo<CheckpointService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckpointScoreService>().AsSingle();
            Container.BindInterfacesTo<CheckpointsPool>().AsSingle();
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            Container.BindInterfacesAndSelfTo<DebugService>().AsSingle();
#endif
        }

        private void BindLightService()
        {
            Container.BindInterfacesTo<LightService>().AsSingle();

            Container.Bind<ILightActionHandler>().To<LightActionHandler>().AsSingle();
            Container.Bind<ChangeColorLightEventAction>().AsSingle();
            Container.Bind<DelayLightEventAction>().AsSingle();
        }

        private void BindWindows()
        {
            Container.DeclareSignal<MainMenuWindow>();
            Container.BindInterfacesAndSelfTo<MainMenuWindow>().AsSingle();

            Container.DeclareSignal<GameHudWindow>();
            Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();

            Container.DeclareSignal<DeathWindow>();
            Container.BindInterfacesAndSelfTo<DeathWindow>().AsSingle();

            Container.DeclareSignal<ReviveWindow>();
            Container.BindInterfacesAndSelfTo<ReviveWindow>().AsSingle();

            Container.DeclareSignal<SettingsPopupWindow>();
            Container.BindInterfacesAndSelfTo<SettingsPopupWindow>().AsSingle();
            
            Container.DeclareSignal<TutorialWindow>();
            Container.BindInterfacesAndSelfTo<TutorialWindow>().AsSingle();
            
            Container.DeclareSignal<TutorialWithCollectablesWindow>();
            Container.BindInterfacesAndSelfTo<TutorialWithCollectablesWindow>().AsSingle();
            
            Container.DeclareSignal<TutorialFailWindow>();
            Container.BindInterfacesAndSelfTo<TutorialFailWindow>().AsSingle();
            
            Container.DeclareSignal<EmptyWindow>();
            Container.BindInterfacesAndSelfTo<EmptyWindow>().AsSingle();
            
            Container.DeclareSignal<QteWindow>();
            Container.BindInterfacesAndSelfTo<QteWindow>().AsSingle();
        }

        private void BindManagers()
        {
            Container.BindInterfacesTo<MoveController>().AsSingle();
            Container.BindInterfacesTo<LinkedEntityRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdAnalyticsManager>().AsSingle();
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            Container.BindInterfacesAndSelfTo<KeyboardInputService>().AsSingle().NonLazy();
#endif
        }
    }
}