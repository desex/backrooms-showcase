using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 110, nameof(EFeatures.Move))]
    public class StartPlayerTurnSystem : ForEachCommandUpdateSystem<StartPlayerTurnCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerTurningParametersBase _playerTurningParametersBase;

        public StartPlayerTurnSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerTurningParametersBase playerTurningParametersBase
        ) : base(commandBuffer)
        {
            _game = game;
            _playerTurningParametersBase = playerTurningParametersBase;
        }

        protected override void Execute(ref StartPlayerTurnCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (!CanUse(character))
                return;

            if (!TryGetTurnDirection(character, out var turnDirection))
                return;

            var playerTransform = character.Transform.Value;
            var playerRotationEuler = playerTransform.rotation.eulerAngles;
            var currentAngle = playerRotationEuler.y;

            var turningAngle = _playerTurningParametersBase.TurningAngle;
            var dir = turningAngle * Mathf.Sign(turnDirection == ETurnDirection.Left ? -1 : 1);

            var newTargetAngle = currentAngle + dir;

            character.ReplaceTurning(new TurningValues(newTargetAngle, 0f, dir > 0, true));
            character.IsAvailableTutorialTurn = false;
            character.IsReadyToTurn = false;
        }

        private static bool TryGetTurnDirection(GameEntity character, out ETurnDirection? direction)
        {
            direction = null;
            var horizontalDirection = character.HorizontalMoveDirection.Value;

            if (horizontalDirection == EHorizontalMoveDirection.None)
                return false;

            ETurnDirection? turnDirection = horizontalDirection switch
            {
                EHorizontalMoveDirection.Left => ETurnDirection.Left,
                EHorizontalMoveDirection.Right => ETurnDirection.Right,
                _ => null,
            };

            if (character.HasTurning)
                return false;

            if (!character.IsReadyToTurn)
                return false;

            var availableTurnDirection = character.TurnChunkData.Value.Direction;

            if (availableTurnDirection != turnDirection && availableTurnDirection != ETurnDirection.Both)
                return false;

            direction = turnDirection!.Value;

            return true;
        }

        private static bool CanUse(GameEntity character)
        {
            var isTurnBlocked = character.IsPlayer
                                && character.BlockedMovements.Value.IsMovementBlocked(EBlockedMovement.Turn);

            if (isTurnBlocked)
                return false;

            if (character.IsDead)
                return false;

            if (!character.HasTurnChunkData)
                return false;

            if (!character.HasHorizontalMoveDirection)
                return false;

            return true;
        }
    }
}