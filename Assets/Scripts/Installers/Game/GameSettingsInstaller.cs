using Db.Ai;
using Db.Ai.Impl;
using Db.Analytics;
using Db.Analytics.Impl;
using Db.BerserkMode;
using Db.BerserkMode.Impl;
using Db.Camera;
using Db.Camera.Impl;
using Db.ChasePlayer;
using Db.ChasePlayer.Impl;
using Db.Checkpoints;
using Db.Checkpoints.Impl;
using Db.CollectablesItem;
using Db.CollectablesItem.Impl;
using Db.CurvedWorld;
using Db.CurvedWorld.Impl;
using Db.Death;
using Db.Death.Impl;
using Db.Effects;
using Db.Effects.Impl;
using Db.Fade;
using Db.Fade.Impl;
using Db.Fx;
using Db.Fx.Impl;
using Db.Ghost;
using Db.Ghost.Impl;
using Db.Graphics;
using Db.Graphics.Impl;
using Db.LampHiders;
using Db.LampHiders.Impl;
using Db.Lights.Impl;
using Db.MainMenu;
using Db.MainMenu.Impl;
using Db.Monsters;
using Db.Monsters.Impl;
using Db.Obstacles;
using Db.Obstacles.DestructibleObstacles;
using Db.Obstacles.DestructibleObstacles.Impl;
using Db.Obstacles.Impls;
using Db.PassObstacleFeedback;
using Db.Player;
using Db.Player.Impl;
using Db.Prefabs;
using Db.Prefabs.Impls;
using Db.QTE;
using Db.QTE.Impl;
using Db.Render.Glitch;
using Db.Render.Glitch.Impl;
using Db.Render.SpeedShader;
using Db.Render.SpeedShader.Impl;
using Db.ReviveView;
using Db.ReviveView.Impl;
using Db.Score;
using Db.Score.Impl;
using Db.Screamers;
using Db.Screamers.Impl;
using Db.Settings;
using Db.Settings.Impl;
using Db.Sound;
using Db.Sound.Impl;
using Db.SoundFxBase;
using Db.SoundFxBase.Impl;
using Db.SoundScreamer;
using Db.SoundScreamer.Impl;
using Db.TimeScale;
using Db.TimeScale.Impl;
using Db.TransitionChunk;
using Db.TransitionChunk.Impl;
using Db.TransitionHoleChunk;
using Db.TransitionHoleChunk.Impl;
using Db.Transitions;
using Db.Transitions.Impl;
using Db.Tutorial;
using Db.Tutorial.Impl;
using Db.TvLines;
using Db.TvLines.Impl;
using Db.UI.Checkpoint;
using Db.UI.Checkpoint.Impl;
using Db.UI.Collectables;
using Db.UI.Collectables.Impl;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Db.Debugs;
using Db.Debugs.Impl;
#endif
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Db.PlayerInput;
using Db.PlayerInput.Impl;
#endif

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AdaptivePerformanceSettingsBase _adaptivePerformanceSettingsBase;
        [SerializeField] private CameraParameters cameraParameters;
        [SerializeField] private PrefabsBase prefabsBase;
        [SerializeField] private FxObjectBase fxObjectBase;
        [SerializeField] private CollectablesItemBase _collectablesItemBase;
        [SerializeField] private AiBTreeSettingsBase _aiBTreeSettingsBase;
        [SerializeField] private ObstacleParametersBase obstacleParametersBase;
        [SerializeField] private ScoreParameters scoreParameters;
        [SerializeField] private LampHiderModuleSettings _lampHiderModuleSettings;
        [SerializeField] private MonsterScreamerParametersBase _monsterScreamerParameters;
        [SerializeField] private LightSettings _lightParameters;
        [SerializeField] private TransitionSettingsBase _transitionSettings;
        [SerializeField] private ChaserPresentationParameters _chaserPresentationParameters;
        [SerializeField] private GhostsParameters _ghostsParameters;
        [SerializeField] private CheckpointsParameters _checkpointsParameters;
        [SerializeField] private TravelatorParameters _travelatorParameters;
        [SerializeField] private PassObstacleFeedbackColorParameters _passObstacleFeedbackColorParameters;
        [SerializeField] private BreakBoxParameters _breakBoxParameters;
        [SerializeField] private TutorialParameters _tutorialParameters;
        [SerializeField] private AnalyticsParameters _analyticsParameters;
        
        [Space]
        [Header("Ui parameters")]
        [SerializeField] private MainMenuAnimationParameters _mainMenuAnimationParameters;
        [SerializeField] private CollectablesViewParameters _collectablesViewParameters;
        [SerializeField] private ReviveViewSettings _reviveViewSettings;
        [SerializeField] private FadeParametersBase _fadeParametersBase;
        [SerializeField] private UiEffectsParametersBase _uiEffectsParametersBase;
        [SerializeField] private DeadBlackoutParameters _deadBlackoutParameters;
        [SerializeField] private ReactionObstacleEffectBase _reactionObstacleEffectBase;
        [SerializeField] private SpeedEffectParameters _speedEffectParameters;
        [SerializeField] private AccelerationFeedbackParameters _accelerationFeedbackParameters;
        [SerializeField] private CheckpointViewParameters _checkpointViewParameters;
        [SerializeField] private DashTutorialParametersBase _dashTutorialParametersBase;
        
        [Space]
        [Header("Shaders parameters")]
        [SerializeField] private TvLinesParameters _tvLinesParameters;
        [SerializeField] private GlitchParameters _glitchParameters;
        [SerializeField] private BorderedSpeedShaderParameters _borderedSpeedShaderParameters;
        [Space]
        [Header("Sound parameters")]
        [SerializeField] private ScreamerParametersParametersBase _screamerParametersBase;
        [SerializeField] private SoundSettingsBase _soundSettingsBase;
        [SerializeField] private ChaserSoundParameters _chaserSoundParameters;
        [SerializeField] private SoundFxBase _soundFxBase;
        
        [Space]
        [Header("Player parameters")]
        [SerializeField] private BerserkModeParameters _berserkModeParameters;
        [SerializeField] private PlayerBasicMovementParametersBase playerBasicMovementParametersBase;
        [SerializeField] private PlayerWaterInHoleParametersBase _playerWaterInHoleParametersBase;
        [SerializeField] private PlayerHealthParametersBase playerHealthParametersBase;
        [SerializeField] private PlayerMonkeyJumpParametersBase playerMonkeyJumpParametersBase;
        [SerializeField] private PlayerWallMotionParametersBase playerWallMotionParametersBase;
        [SerializeField] private PlayerSlideAndJumpParameters _playerSlideParameters;
        [SerializeField] private PlayerTurningParametersBase playerTurningParametersBase;
        [SerializeField] private SettingsParameterBase _settingsParameterBase;
        [SerializeField] private PlayerForwardJumpParametersBase _playerForwardJumpParametersBase;
        [SerializeField] private PlayerInvulnerabilityReboundParameters _playerInvulnerabilityReboundParameters;
        [SerializeField] private PlayerJumpOverObstacleParametersBase _playerJumpOverObstacleParametersBase;
        [SerializeField] private ReboundParameters _reboundParameters;
        [SerializeField] private PlayerJumpOnParametersBase _playerJumpOnParametersBase;
        [SerializeField] private PlayerPlankRunParametersBase _playerPlankRunParametersBase;
        [SerializeField] private PlayerInputParametersBase _playerInputParametersBase;
        [SerializeField] private PlayerAccelerationParametersBase _playerAccelerationParametersBase;
        [SerializeField] private PlayerDeathParametersBase _playerDeathParametersBase;
        [SerializeField] private PlayerClimbParametersBase _playerClimbParametersBase;
        [SerializeField] private PlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        [SerializeField] private DashParameters _dashParameters;
        [SerializeField] private PlayerDoubleTapParameters _doubleTapParameters;
        [SerializeField] private PlayerDoubleTapFxParameters _doubleTapFxParameters;
        [SerializeField] private OutOfBoundsKillPlayerParameters _outOfBoundsKillPlayerParameters;
        [SerializeField] private ChasePlayerParameters _chasePlayerParameters;
        [SerializeField] private CurvedWorldTypeTypeParameters _curvedWorldTypeParameters;
        [SerializeField] private SlowingDownReviveParameters slowingDownReviveParameters;
        [SerializeField] private QteParameters _qteParameters;
        
        [Space]
        [Header("Obstacles")]
        [SerializeField] private DestructibleObstaclesParameters _destructibleObstaclesParameters;

        [Space]
        [Header("Chunk")]
        [SerializeField] private TransitionChunkParameters _transitionChunkParameters;
        [SerializeField] private TransitionHoleParameters _transitionHoleParameters;

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        [Space]
        [Header("Player input")]
        [SerializeField] 
        private KeyboardInputParameters _keyboardInputParameters;

        [Space]
        [Header("Debug / QA")]
        [SerializeField] 
        private DebugParameters _debugParameters;
#endif

        public override void InstallBindings()
        {
            Container.Bind<IAdaptivePerformanceSettingsBase>().FromSubstitute(_adaptivePerformanceSettingsBase).AsSingle();
            Container.Bind<ICameraParameters>().FromSubstitute(cameraParameters).AsSingle();
            Container.Bind<IPrefabsBase>().FromSubstitute(prefabsBase).AsSingle();
            Container.Bind<IFxObjectBase>().FromSubstitute(fxObjectBase).AsSingle();
            Container.Bind<ICollectablesItemBase>().FromSubstitute(_collectablesItemBase).AsSingle();
            Container.Bind<ICheckpointsParameters>().FromSubstitute(_checkpointsParameters).AsSingle();
            Container.Bind<ITravelatorParameters>().FromSubstitute(_travelatorParameters).AsSingle();

            Container.Bind<IAiBTreeSettingsBase>().FromSubstitute(_aiBTreeSettingsBase).AsSingle();
            Container.Bind<IObstacleParametersBase>().FromSubstitute(obstacleParametersBase).AsSingle();
            Container.Bind<IScoreParameters>().FromSubstitute(scoreParameters).AsSingle();
            Container.Bind<ILampHiderModuleSettings>().FromInstance(_lampHiderModuleSettings).AsSingle();
            Container.Bind<IMonsterScreamerParameters>().FromInstance(_monsterScreamerParameters).AsSingle();
            Container.Bind<ITransitionSettingsBase>().FromInstance(_transitionSettings).AsSingle();
            Container.Bind<IPassObstacleFeedbackParameters>().FromInstance(_passObstacleFeedbackColorParameters).AsSingle();
            Container.Bind<IBreakBoxParameters>().FromInstance(_breakBoxParameters).AsSingle();
            Container.Bind<IChaserPresentationParameters>().FromInstance(_chaserPresentationParameters).AsSingle();
            Container.Bind<ITutorialParameters>().FromSubstitute(_tutorialParameters).AsSingle();
            Container.Bind<IGhostsParameters>().FromSubstitute(_ghostsParameters).AsSingle();
            Container.Bind<IAnalyticsParameters>().FromSubstitute(_analyticsParameters).AsSingle();
            Container.Bind<ISlowingDownReviveParameters>().FromInstance(slowingDownReviveParameters).AsSingle();
            
            //UI
            Container.Bind<IMainMenuAnimationParameters>().FromInstance(_mainMenuAnimationParameters).AsSingle();
            Container.Bind<ICollectablesViewParameters>().FromInstance(_collectablesViewParameters).AsSingle();
            Container.Bind<IReviveViewSettings>().FromInstance(_reviveViewSettings).AsSingle();
            Container.Bind<IFadeParameters>().FromInstance(_fadeParametersBase).AsSingle();
            Container.Bind<IUiEffectsParameters>().FromSubstitute(_uiEffectsParametersBase).AsSingle();
            Container.Bind<IDeadBlackoutParameters>().FromSubstitute(_deadBlackoutParameters).AsSingle();
            Container.Bind<IReactionObstacleEffectBase>().FromSubstitute(_reactionObstacleEffectBase).AsSingle();
            Container.Bind<IAccelerationFeedbackParameters>().FromSubstitute(_accelerationFeedbackParameters).AsSingle();
            Container.Bind<ICheckpointViewParameters>().FromSubstitute(_checkpointViewParameters).AsSingle();
            Container.Bind<IDashTutorialParameters>().FromSubstitute(_dashTutorialParametersBase).AsSingle();
            
            //Shaders
            Container.Bind<ITvLinesParameters>().FromInstance(_tvLinesParameters).AsSingle();
            Container.Bind<IGlitchParameters>().FromInstance(_glitchParameters).AsSingle();
            Container.Bind<IBorderedSpeedShaderParameters>().FromInstance(_borderedSpeedShaderParameters).AsSingle();
            Container.Bind<ISpeedEffectParameters>().FromSubstitute(_speedEffectParameters).AsSingle();
            
            //Sound parameters
            Container.Bind<IScreamerParametersBase>().FromSubstitute(_screamerParametersBase).AsSingle();
            Container.Bind<ISoundFxBase>().FromInstance(_soundFxBase).AsSingle();
            Container.Bind<ISoundSettingsBase>().FromSubstitute(_soundSettingsBase).AsSingle();
            Container.Bind<IChaserSoundParameters>().FromSubstitute(_chaserSoundParameters).AsSingle();
            
            //Player parameters
            Container.Bind<IBerserkModeParameters>().FromSubstitute(_berserkModeParameters).AsSingle();
            Container.Bind<IPlayerBasicMovementParametersBase>().FromSubstitute(playerBasicMovementParametersBase).AsSingle();
            Container.Bind<IPlayerWaterInHoleParametersBase>().FromSubstitute(_playerWaterInHoleParametersBase).AsSingle();
            Container.Bind<IPlayerHealthParametersBase>().FromSubstitute(playerHealthParametersBase).AsSingle();
            Container.Bind<IPlayerMonkeyJumpParametersBase>().FromSubstitute(playerMonkeyJumpParametersBase).AsSingle();
            Container.Bind<IPlayerWallMotionParametersBase>().FromSubstitute(playerWallMotionParametersBase).AsSingle();
            Container.Bind<IPlayerSlideAndJumpParameters>().FromSubstitute(_playerSlideParameters).AsSingle();
            Container.Bind<IPlayerTurningParametersBase>().FromSubstitute(playerTurningParametersBase).AsSingle();
            Container.Bind<IPlayerForwardJumpParametersBase>().FromSubstitute(_playerForwardJumpParametersBase).AsSingle();
            Container.Bind<IPlayerInvulnerabilityReboundParameters>().FromSubstitute(_playerInvulnerabilityReboundParameters).AsSingle();
            Container.Bind<IPlayerJumpOverObstacleParametersBase>().FromSubstitute(_playerJumpOverObstacleParametersBase).AsSingle();
            Container.Bind<IReboundParameters>().FromSubstitute(_reboundParameters).AsSingle();
            Container.Bind<ISettingsParameter>().FromSubstitute(_settingsParameterBase).AsSingle();
            Container.Bind<IPlayerJumpOnParametersBase>().FromSubstitute(_playerJumpOnParametersBase).AsSingle();
            Container.Bind<IPlayerPlankRunParametersBase>().FromSubstitute(_playerPlankRunParametersBase).AsSingle();
            Container.Bind<IPlayerInputParametersBase>().FromSubstitute(_playerInputParametersBase).AsSingle();
            Container.Bind<IPlayerAccelerationParametersBase>().FromSubstitute(_playerAccelerationParametersBase).AsSingle();
            Container.Bind<IPlayerDeathParametersBase>().FromSubstitute(_playerDeathParametersBase).AsSingle();
            Container.Bind<IPlayerClimbParametersBase>().FromSubstitute(_playerClimbParametersBase).AsSingle();
            Container.Bind<IPlayerSpeedProgressionParameters>().FromSubstitute(_playerSpeedProgressionParameters).AsSingle();
            Container.Bind<IDashParameters>().FromSubstitute(_dashParameters).AsSingle();
            Container.Bind<IPlayerDoubleTapParameters>().FromSubstitute(_doubleTapParameters).AsSingle();
            Container.Bind<IPlayerDoubleTapFxParameters>().FromSubstitute(_doubleTapFxParameters).AsSingle();
            Container.Bind<IOutOfBoundsKillPlayerParameters>().FromSubstitute(_outOfBoundsKillPlayerParameters).AsSingle();
            Container.Bind<IChasePlayerParameters>().FromSubstitute(_chasePlayerParameters).AsSingle();
            Container.Bind<IQteParameters>().FromSubstitute(_qteParameters).AsSingle();
            
            //Chunk
            Container.Bind<ITransitionChunkParameters>().FromSubstitute(_transitionChunkParameters).AsSingle();
            Container.Bind<ITransitionHoleParameters>().FromSubstitute(_transitionHoleParameters).AsSingle();
            Container.Bind<IDestructibleObstaclesParameters>().FromSubstitute(_destructibleObstaclesParameters).AsSingle();      
            Container.Bind<ICurvedWorldTypeParameters>().FromSubstitute(_curvedWorldTypeParameters).AsSingle();

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            //Player input
            Container.Bind<IKeyboardInputParameters>().FromSubstitute(_keyboardInputParameters).AsSingle();
            Container.Bind<IDebugParameters>().FromSubstitute(_debugParameters).AsSingle();
#endif
        }
    }
}