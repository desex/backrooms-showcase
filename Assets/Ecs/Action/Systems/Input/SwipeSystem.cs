using Db.Player;
using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Game.Services.Buffer;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 500, nameof(EFeatures.Input))]
    public class SwipeSystem : ForEachCommandUpdateSystem<SwipeCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerInputParametersBase _playerInputParametersBase;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;
        private readonly IBufferSwipeService _bufferSwipe;

        public SwipeSystem(
            ICommandBuffer commandBuffer,
            IPlayerInputParametersBase playerInputParametersBase,
            IPlayerSlideAndJumpParameters playerSlideAndJumpParameters,
            GameContext game,
            IBufferSwipeService bufferSwipe
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _playerInputParametersBase = playerInputParametersBase;
            _game = game;
            _bufferSwipe = bufferSwipe;
            _playerSlideAndJumpParameters = playerSlideAndJumpParameters;
        }

        protected override void Execute(ref SwipeCommand command)
        {
            var player = _game.PlayerEntity;

            if (!player.IsPlayerControl)
                return;

            if (!player.IsCanSwipe)
                return;

            if (player.IsDead)
                return;

            var swipeDirection = command.Direction;
            var swipeThreshold = _playerSlideAndJumpParameters.SwipeThreshold;
            var ratio = _playerInputParametersBase.VerticalToHorizontalSwipeRatio;
            
            _bufferSwipe.BufferSwipe(player, swipeDirection.y, swipeThreshold);

            if (Mathf.Abs(swipeDirection.y) / Mathf.Abs(swipeDirection.x) > ratio)
                HandleDiagonalSwipe();
            else
                HandleHorizontalSwipe(swipeDirection.x, swipeThreshold);

            return;

            void HandleDiagonalSwipe()
            {
                if (!player.IsCanSwipeVertical)
                    return;

                HandleHorizontalSwipe(swipeDirection.x, swipeThreshold);
                HandleUpSwipe(swipeDirection.y, swipeThreshold);
                HandleDownSwipe(swipeDirection.y, swipeThreshold);
            }
        }

        private void HandleHorizontalSwipe(float swipeDirectionX, float swipeThreshold)
        {
            var player = _game.PlayerEntity;

            if (!player.HasCharacterTouchWall)
                return;

            HandleLeftSwipe(swipeDirectionX, swipeThreshold);
            HandleRightSwipe(swipeDirectionX, swipeThreshold);
        }

        private void HandleUpSwipe(float swipeDirectionY, float swipeThreshold)
        {
            if (swipeDirectionY <= swipeThreshold)
                return;

            var player = _game.PlayerEntity;
            var playerUid = player.Uid.Value;
            var isReadyForJumpOverObstacles = player.IsReadyForJumpOverObstacles;
            var isReadyToJumpOn = player.HasReadyToJumpOn;
            var isShortSlide = player.HasSliding && player.Sliding.Type == ESlideType.Short;
            var isControllableSlide = player.HasSliding && player.Sliding.Type == ESlideType.Controllable;
            var isInCorrectJumpForDoubleJump = IsInCorrectJumpForDoubleJump(player);
            var isCanObstaclePass = IsCorrectObstaclePassType(EPassObstacleType.Up, player);
            var tryPassObstacleInAir = IsCorrectObstaclePassType(EPassObstacleType.Up, player) && player.IsFallInterrupted;
            var jumpHeight = _playerSlideAndJumpParameters.JumpHeight;
            

            if (isControllableSlide)
            {
                _commandBuffer.InterruptControllableSlide(playerUid);
                _commandBuffer.Jump(playerUid, jumpHeight);
                return;
            }
            
            if (tryPassObstacleInAir)
            {
                player.ReplaceObstacleActivationOnLanding(EPassObstacleType.Up);
            }

            if (isShortSlide && !isCanObstaclePass)
            {
                _commandBuffer.CancelSlide(playerUid);
                _commandBuffer.Jump(playerUid, jumpHeight);
            }
            else if (isCanObstaclePass && !player.IsFallInterrupted)
            {
                _commandBuffer.TryDashWhenInObstacle(playerUid, EPassObstacleType.Up);
            }
            else if (isReadyForJumpOverObstacles)
            {
                _commandBuffer.JumpOverObstacle();
            }
            else if (isReadyToJumpOn)
            {
                _commandBuffer.JumpOn();
            }
            else if (isInCorrectJumpForDoubleJump && !player.IsFallingInHole)
            {
                _commandBuffer.DoubleJump(playerUid);
            }
            else if (!player.IsFallingInHole)              
            {
                if (player.HasJumping && player.Jumping.Value == EJumpType.DoubleJump)
                    return;
                
                _commandBuffer.Jump(playerUid, jumpHeight);
            }
        }

        private void HandleDownSwipe(float swipeDirectionY, float swipeThreshold)
        {
            if (swipeDirectionY >= -swipeThreshold)
                return;

            var player = _game.PlayerEntity;
            var playerUid = player.Uid.Value;
            var isGrounded = player.IsGrounded;
            var isSliding = player.HasSlideDuration;
            var isCanObstaclePass = IsCorrectObstaclePassType(EPassObstacleType.Down, player);

            if (isCanObstaclePass && isGrounded)
                _commandBuffer.TryDashWhenInObstacle(playerUid, EPassObstacleType.Down);
            else if (isSliding)
                _commandBuffer.ContinueSlide();
            else if (isGrounded)
                _commandBuffer.StartSlide(playerUid);
            else
            {
                player.ReplaceObstacleActivationOnLanding(EPassObstacleType.Down);
                _commandBuffer.InterruptFall(playerUid);
            }
        }

        private void HandleLeftSwipe(float swipeDirectionX, float swipeThreshold)
        {
            if (swipeDirectionX >= -swipeThreshold)
                return;

            _commandBuffer.WallJump(swipeDirectionX);
        }

        private void HandleRightSwipe(float swipeDirectionX, float swipeThreshold)
        {
            if (swipeDirectionX <= swipeThreshold)
                return;

            _commandBuffer.WallJump(swipeDirectionX);
        }

        private static bool IsInCorrectJumpForDoubleJump(GameEntity player)
        {
            if (!player.HasJumping)
                return false;

            return player.Jumping.Value is EJumpType.Jump or EJumpType.JumpOverObstacle;
        }

        private static bool IsCorrectObstaclePassType(EPassObstacleType type, GameEntity player)
        {
            if (!player.HasReadyToObstaclePass)
                return false;

            if (!player.IsCanDash)
                return false;

            if (!player.HasPassObstacleMovePoints)
                return false;

            if (player.IsDead)
                return false;

            var passObstacleData = player.PassObstacleMovePoints.Value;
            var movePoints = passObstacleData.MovePoints;

            foreach (var pointData in movePoints)
            {
                if (pointData.PassObstacleType == type)
                    return true;
            }

            return false;
        }
    }
}