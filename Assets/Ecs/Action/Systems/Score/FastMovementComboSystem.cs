using Ecs.Action.Commands.Movement;
using Ecs.Utils;
using Game.Services.Score;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Score
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 769, nameof(EFeatures.Score))]
    public class FastMovementComboSystem : ForEachCommandUpdateSystem<EnableFastMovementActionCommand>
    {
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;

        protected override bool CleanUp => false;

        public FastMovementComboSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IScoreService scoreService
        ) : base(commandBuffer)
        {
            _game = game;
            _scoreService = scoreService;
        }

        protected override void Execute(ref EnableFastMovementActionCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (character.IsDead || character.IsDestroyed)
                return;

            var isMovementFast = character.IsReadyForFastActionMove;

            var actionType = isMovementFast ? EScoreActionType.FastZoneAction : EScoreActionType.NormalZoneAction;
            _scoreService.HandleSuccessAction(actionType, command.CharacterUid);
        }
    }
}