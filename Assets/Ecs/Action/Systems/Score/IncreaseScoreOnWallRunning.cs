using Ecs.Action.Commands;
using Ecs.Core;
using Ecs.Utils;
using Game.Services.Score;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Score
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 669, nameof(EFeatures.Score))]
    public class IncreaseScoreOnWallRunning : AConditionalForEachCommandUpdateSystem<SetReadyWallRunCommand>
    {
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;

        protected override bool CleanUp => false;

        public IncreaseScoreOnWallRunning(
            ICommandBuffer commandBuffer,
            IScoreService scoreService,
            GameContext game
        ) : base(commandBuffer)
        {
            _scoreService = scoreService;
            _game = game;
        }

        protected override bool ConditionFilter()
        {
            var player = _game.PlayerEntity;
            return !player.IsDead && !player.IsDestroyed;
        }

        protected override void ExecuteCommand(ref SetReadyWallRunCommand command)
        {
            if (!command.IsReady)
                return;

            _scoreService.HandleSuccessAction(EScoreActionType.ActionWithoutZones, command.CharacterUid);
        }
    }
}