using Core.Utils;
using Ecs.Action.Commands.Tutorial;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.GameState;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.PlayerPrefs;
#if !DISABLE_GHOSTS
using Generated.Commands;
#endif

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 930, nameof(EFeatures.Chunk))]
    public class FinishTutorialSystem : ForEachCommandUpdateSystem<FinishTutorialCommand>
    {
#if !DISABLE_GHOSTS
        private readonly ICommandBuffer _commandBuffer;
#endif
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly IPlayerPrefsManager _playerPrefsManager;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;

        public FinishTutorialSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IGameStateService gameStateService,
            IPlayerPrefsManager playerPrefsManager,
            ITutorialAnalyticsService tutorialAnalyticsService
        ) : base(commandBuffer)
        {
#if !DISABLE_GHOSTS
            _commandBuffer = commandBuffer;
#endif
            _game = game;
            _gameStateService = gameStateService;
            _playerPrefsManager = playerPrefsManager;
            _tutorialAnalyticsService = tutorialAnalyticsService;
        }

        protected override void Execute(ref FinishTutorialCommand command)
        {
            _game.TutorialEntity.IsInTutorial = false;
            _game.PlayerEntity.ReplaceBlockedMovements(EBlockedMovement.None);
            _playerPrefsManager.SetValue(SingleValueSavePathKeys.WAS_TUTORIAL_PASSED, true);
            _tutorialAnalyticsService.HandleTutorialsFinish();
            _gameStateService.EnterGameplay();
#if !DISABLE_GHOSTS
            _commandBuffer.SpawnGhosts();
#endif
        }
    }
}