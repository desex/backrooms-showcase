using Ecs.Action.Commands.Health;
using Game.Services.Player;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Health
{
    public abstract class AReviveSystem : ForEachCommandUpdateSystem<ReviveCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _movementTypeService;

        protected override bool CleanUp => false;

        protected AReviveSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService movementTypeService
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _movementTypeService = movementTypeService;
        }

        protected abstract bool IsNeedCharacter(GameEntity character);
        protected abstract void HandleRevive(GameEntity character);

        protected override void Execute(ref ReviveCommand command)
        {
            var character = _game.GetEntityWithUid(command.TargetUid);

            if (!IsNeedCharacter(character))
                return;

            character.Transform.Value.SetParent(null);

            if (character.IsDamagedVisually)
                _commandBuffer.RemoveDamageVisuals(character.Uid.Value);

            ResetCharacter(character);

            ResetMovementComponents(character);

            ResetPhysics(character);

            _movementTypeService.UpdateBodyType(character.Uid.Value);

            HandleRevive(character);
        }

        private static void ResetCharacter(GameEntity player)
        {
            player.IsCanBeDamaged = true;
            player.IsDamaged = false;
            player.IsDead = false;
            player.IsOutOfBoundsKillPlayer = false;
            player.IsBerserkMode = false;
        }

        private static void ResetPhysics(GameEntity player)
        {
            player.IsInWater = false;
            player.IsUseGravity = true;
            player.IsGrounded = true;
            player.IsUseExtraGravity = false;

            player.ReplaceVelocity(Vector3.zero);

            player.RemoveHorizontalSpeed();

            if (player.HasHorizontalMoveDirection)
                player.RemoveHorizontalMoveDirection();

            if (player.HasVerticalMoveDirection)
                player.RemoveVerticalMoveDirection();

            if (player.HasPointerHold)
                player.RemovePointerHold();

            if (player.HasForwardSpeed)
                player.RemoveForwardSpeed();
        }

        private static void ResetMovementComponents(GameEntity player)
        {
            ResetIsInterruptMovementComponents(player);

            ResetSliding(player);

            ResetTurning(player);

            ResetJumping(player);

            ResetClimbing(player);

            ResetWallRun(player);

            ResetPlankRun(player);

            ResetObstaclePass(player);

            if (player.HasMoveAnimationDuration)
                player.RemoveMoveAnimationDuration();

            if (player.HasHoleDashTrigger)
                player.RemoveHoleDashTrigger();

            if (player.HasClimbInHole)
                player.RemoveClimbInHole();

            player.IsFallInterrupted = false;

            player.IsReadyForFastActionMove = false;
        }

        private static void ResetIsInterruptMovementComponents(GameEntity player)
        {
            player.IsInterruptPlayerMovement = false;
        }

        private static void ResetTurning(GameEntity player)
        {
            if (player.HasTurning)
                player.RemoveTurning();

            if (player.IsReadyToTurn)
                player.IsReadyToTurn = false;
        }

        private static void ResetClimbing(GameEntity player)
        {
            player.IsClimbing = false;

            if (player.HasClimbParameters)
                player.RemoveClimbParameters();

            if (player.HasReadyToClimb)
                player.RemoveReadyToClimb();
        }

        private static void ResetPlankRun(GameEntity player)
        {
            if (player.HasPlankRun)
                player.RemovePlankRun();
        }

        private static void ResetWallRun(GameEntity player)
        {
            if (player.HasWallRun)
                player.RemoveWallRun();

            if (player.HasCharacterTouchWall)
                player.RemoveCharacterTouchWall();
        }

        private static void ResetJumping(GameEntity player)
        {
            if (player.HasJumping)
                player.RemoveJumping();

            if (player.HasJumpOnMove)
                player.RemoveJumpOnMove();

            if (player.HasJumpOnDuration)
                player.RemoveJumpOnDuration();

            if (player.HasReadyToJumpOn)
                player.RemoveReadyToJumpOn();
        }

        private static void ResetSliding(GameEntity player)
        {
            if (player.HasSliding)
                player.RemoveSliding();

            if (player.HasSlideDuration)
                player.RemoveSlideDuration();
        }

        private static void ResetObstaclePass(GameEntity player)
        {
            if (player.HasPassObstacleMovePoints)
                player.RemovePassObstacleMovePoints();

            if (player.HasReadyToObstaclePass)
                player.RemoveReadyToObstaclePass();

            if (player.HasObstaclePassReact)
                player.RemoveObstaclePassReact();

            if (player.HasDashWhenInObstacle)
                player.RemoveDashWhenInObstacle();
        }
    }
}