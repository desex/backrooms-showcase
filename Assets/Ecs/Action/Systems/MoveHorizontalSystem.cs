using Db.Player;
using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 940, nameof(EFeatures.Move))]
    public class MoveHorizontalSystem : ForEachCommandUpdateSystem<MoveHorizontalCommand>
    {
        private readonly GameContext _game;
        private readonly IPlayerInputParametersBase _playerInputParametersBase;
        private readonly IPlayerSpeedService _playerSpeedService;

        public MoveHorizontalSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerSpeedService playerSpeedService,
            IPlayerInputParametersBase playerInputParametersBase
        ) : base(commandBuffer)
        {
            _game = game;
            _playerSpeedService = playerSpeedService;
            _playerInputParametersBase = playerInputParametersBase;
        }

        protected override void Execute(ref MoveHorizontalCommand command)
        {
            var player = _game.PlayerEntity;

            if (!IsReadyForControl(player))
                return;

            var delta = command.NormalizedDelta;

            if (Mathf.Abs(delta) < _playerInputParametersBase.JoystickNormalizedDeadZone)
            {
                player.ReplaceHorizontalMoveDirection(EHorizontalMoveDirection.None);
                return;
            }

            var direction = delta > 0 ? EHorizontalMoveDirection.Right : EHorizontalMoveDirection.Left;

            var normalizedRadius = _playerInputParametersBase.JoystickNormalizedRadius;
            var deltaLength = Mathf.Abs(delta / normalizedRadius);
            var needDelta = Mathf.Min(deltaLength, 1f) * Mathf.Sign(delta);

            var horizontalSpeed = _playerSpeedService.GetHorizontalSpeed(player) * needDelta;

            player.ReplaceHorizontalMoveDirection(direction);
            player.ReplaceHorizontalSpeed(horizontalSpeed);
        }

        private static bool IsReadyForControl(GameEntity player)
        {
            var bodyType = player.BodyMoveType.Value;
            var isDead = player.IsDead;

            if (player.HasJumping && player.Jumping.Value is EJumpType.Jump or EJumpType.DoubleJump)
                return !isDead;

            var isGrounded = player.IsGrounded;

            var isControllableBodyMoveType = bodyType is EBodyMoveType.Run
                or EBodyMoveType.HitOnObstacle
                or EBodyMoveType.ControllableSlide
                or EBodyMoveType.RotatedBack;

            return isGrounded && !isDead && isControllableBodyMoveType;
        }
    }
}