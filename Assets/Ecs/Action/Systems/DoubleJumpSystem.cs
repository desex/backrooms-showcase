using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 605, nameof(EFeatures.Move))]
    public class DoubleJumpSystem : ForEachCommandUpdateSystem<DoubleJumpCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;

        public DoubleJumpSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }

        protected override void Execute(ref DoubleJumpCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);
            var isInWideHole = character.IsInWideHole;

            if (!CanUse(character))
                return;

            character.ReplaceJumping(EJumpType.DoubleJump);
            var playerJumpHeight = _playerSlideAndJumpParameters.DoubleJumpHeight;
            var playerJumpInHoleHeight = _playerSlideAndJumpParameters.DoubleJumpInHoleHeight;
            var doubleJumpForce = _playerSlideAndJumpParameters.DoubleJumpForce;

            var doubleJumpVerticalSpeed = !isInWideHole
                ? Mathf.Sqrt(playerJumpHeight * doubleJumpForce)
                : Mathf.Sqrt(playerJumpInHoleHeight * doubleJumpForce);

            if (isInWideHole)
                character.IsComeback = true;

            character.ReplaceVerticalSpeed(doubleJumpVerticalSpeed);

            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
        }

        private bool CanUse(GameEntity character)
        {
            if (character.IsGrounded)
                return false;

            if (character.IsComeback)
                return false;

            if (character.HasHoleDashTrigger)
                return false;

            if (character.IsDash)
                return false;

            if (character.HasJumping && character.Jumping.Value == EJumpType.DoubleJump)
                return false;

            var isDoubleJumpBlocked = character.IsPlayer
                                      && character.BlockedMovements.Value.IsMovementBlocked(
                                          EBlockedMovement.DoubleJump
                                      );

            if (isDoubleJumpBlocked)
                return false;

            return true;
        }
    }
}