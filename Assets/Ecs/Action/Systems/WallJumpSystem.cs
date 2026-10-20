using Cysharp.Threading.Tasks;
using Db.Player;
using Ecs.Action.Commands;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using Game.Utils.UniTaskExtensions;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Move))]
    public class WallJumpSystem : ForEachCommandUpdateSystem<WallJumpCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;
        private readonly IPlayerWallMotionParametersBase _playerWallMotionParameters;

        public WallJumpSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerWallMotionParametersBase playerWallMotionParameters,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _playerWallMotionParameters = playerWallMotionParameters;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }

        protected override void Execute(ref WallJumpCommand command)
        {
            var player = _game.PlayerEntity;
            var wallType = player.CharacterTouchWall.WallType;
            var direction = command.Direction;

            switch (direction)
            {
                case > 0 when wallType != ETiltType.Left:
                case < 0 when wallType != ETiltType.Right:
                    return;
            }

            var verticalJumpForce = _playerSlideAndJumpParameters.JumpForce;
            var horizontalJumpForce = _playerWallMotionParameters.JumpForce;
            var jumpHeight = _playerWallMotionParameters.JumpHeight;
            player.ReplaceJumping(EJumpType.Jump);

            var verticalSpeed = Mathf.Sqrt(jumpHeight * verticalJumpForce);
            player.ReplaceVerticalSpeed(verticalSpeed);

            player.ReplaceHorizontalSpeed(horizontalJumpForce * (direction * 2) - 1);

            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
            player.RemoveJumping();

            var delay = _playerWallMotionParameters.DelayAfterJump;

            CancelJumpAfterDelay(delay).ForgetAndWrapExceptions();
        }

        private async UniTask CancelJumpAfterDelay(float delay)
        {
            await UniTaskUtils.PausableDelay(delay, () => _game.Pause.Value);
            CancelJump();
        }

        private void CancelJump()
        {
            var player = _game.PlayerEntity;

            if (player.HasBodyMoveType && player.BodyMoveType.Value == EBodyMoveType.Jump)
                _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }
    }
}