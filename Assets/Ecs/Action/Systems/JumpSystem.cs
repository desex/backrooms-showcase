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
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 600, nameof(EFeatures.Move))]
    public class JumpSystem : ForEachCommandUpdateSystem<JumpCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;

        public JumpSystem(
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

        protected override void Execute(ref JumpCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);
            var playerJumpHeight = command.Height;

            if (!CanUse(character))
                return;

            character.ReplaceJumping(EJumpType.Jump);

            var jumpForce = _playerSlideAndJumpParameters.JumpForce;
            var verticalSpeed = Mathf.Sqrt(playerJumpHeight * jumpForce);
            character.ReplaceVerticalSpeed(verticalSpeed);
            
            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
        }

        private bool CanUse(GameEntity character)
        {
            var isPlayer = character.IsPlayer;
            var isJumpBlocked = isPlayer && character.BlockedMovements.Value.IsMovementBlocked(EBlockedMovement.Jump);

            if (isJumpBlocked)
                return false;

            if (character.IsProhibitionJump)
                return false;

            var isSliding = character.HasBodyMoveType && character.BodyMoveType.Value == EBodyMoveType.MonkeyJump;

            if (isSliding)
                return false;

            if (character.IsComeback)
                return false;
            
            if (character.HasQteType)
                return false;

            return true;
        }
    }
}