using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using Unity.Mathematics;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Move))]
    public class WallRunUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerWallMotionParametersBase _playerWallMotionParameters;
        private readonly ITimeProvider _timeProvider;

        public WallRunUpdateSystem(
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerWallMotionParametersBase playerWallMotionParameters,
            ITimeProvider timeProvider
        ) : base(game)
        {
            _playerMovementTypeService = playerMovementTypeService;
            _playerWallMotionParameters = playerWallMotionParameters;
            _timeProvider = timeProvider;
        }

        protected override bool Filter()
        {
            var player = Game.PlayerEntity;

            return !player.IsDead && player.HasWallRun && !player.IsDestroyed;
        }

        protected override void UpdateSystem()
        {
            var player = Game.PlayerEntity;

            if (!player.PointerHold.Value)
            {
                CancelWallRun(player);
                return;
            }

            if (player.HasJumping && player.Jumping.Value == EJumpType.DoubleJump)
                player.RemoveJumping();

            var wallRunParameters = HandleAccumulatorStep();

            var yVelocity = GetActualVerticalSpeed(wallRunParameters);
            player.ReplaceVerticalSpeed(yVelocity);

            if (wallRunParameters.Accumulator <= 0)
                CancelWallRun(player);
            else
                player.ReplaceWallRun(wallRunParameters);

            return;

            WallRunParameters HandleAccumulatorStep()
            {
                var wallRunParams = player.WallRun.Value;
                var accumulator = wallRunParams.Accumulator;
                accumulator -= _timeProvider.DeltaTime;
                return new WallRunParameters(wallRunParams.MaxDuration, accumulator);
            }
        }

        private void CancelWallRun(GameEntity player)
        {
            player.RemoveWallRun();

            if (player.HasCharacterTouchWall)
                player.RemoveCharacterTouchWall();

            player.IsUseGravity = true;
            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }

        private float GetActualVerticalSpeed(in WallRunParameters wallRunParameters)
        {
            var velocityCurve = _playerWallMotionParameters.VelocityCurve;
            var velocityModifier = _playerWallMotionParameters.VelocityModifier;

            var maxDuration = wallRunParameters.MaxDuration;
            var currentDuration = wallRunParameters.MaxDuration - wallRunParameters.Accumulator;

            var tParameter = math.remap(0, maxDuration, 0, 1, currentDuration);

            var verticalSpeed = velocityCurve.Evaluate(tParameter);
            return verticalSpeed * velocityModifier;
        }
    }
}