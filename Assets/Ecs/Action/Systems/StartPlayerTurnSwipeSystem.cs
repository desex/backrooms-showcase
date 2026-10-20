using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Game.Utils.Movement;
using Game.Utils.Tutorial;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Move))]
    public class StartPlayerTurnSwipeSystem : ForEachCommandUpdateSystem<SwipeCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        protected override bool CleanUp => false;

        public StartPlayerTurnSwipeSystem(ICommandBuffer commandBuffer, GameContext game) : base(commandBuffer)
        {
            _game = game;
            _commandBuffer = commandBuffer;
        }

        protected override void Execute(ref SwipeCommand command)
        {
            var swipeIsHorizontal = Mathf.Abs(command.Direction.x) > Mathf.Abs(command.Direction.y);
            var player = _game.PlayerEntity;
            var tutorialEntity = _game.TutorialEntity;

            if (player.IsDead)
                return;

            if (!player.HasTurnChunkData)
                return;

            if (!swipeIsHorizontal)
                return;

            if (player.HasTurning)
                return;

            if (!player.IsReadyToTurn)
                return;

            if (tutorialEntity.IsInTutorial
                && tutorialEntity.TutorialState.Value == ETutorialState.Turn
                && !player.IsAvailableTutorialTurn)
                return;

            var turnDirection = command.Direction.x < 0
                ? EHorizontalMoveDirection.Left
                : EHorizontalMoveDirection.Right;

            player.ReplaceHorizontalMoveDirection(turnDirection);
            _commandBuffer.StartPlayerTurn(player.Uid.Value);
        }
    }
}