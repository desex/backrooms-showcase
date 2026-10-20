using Db.Camera;
using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Player;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Move))]
    public class PlayerVelocityUpdateSystem : APausableCharacterGameStateConditionUpdateSystem
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ICameraParameters _cameraParameters;
        private readonly IPlayerSpeedService _playerSpeedService;
        private readonly IPlayerTiltingService _playerTiltingService;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerSlideAndJumpParameters _playerSlideParameters;
        private readonly IPlayerWallMotionParametersBase _playerWallMotionParametersBase;
        private readonly IPlayerAnimationService _playerAnimationService;
        private readonly IPlayerWaterInHoleParametersBase _playerWaterInHoleParameters;

        public PlayerVelocityUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            ICameraParameters cameraParameters,
            IPlayerSpeedService playerSpeedService,
            IPlayerTiltingService playerTiltingService,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSlideAndJumpParameters playerSlideParameters,
            IPlayerWallMotionParametersBase playerWallMotionParametersBase,
            IPlayerAnimationService playerAnimationService,
            IPlayerWaterInHoleParametersBase playerWaterInHoleParameters
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _cameraParameters = cameraParameters;
            _playerSpeedService = playerSpeedService;
            _playerTiltingService = playerTiltingService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSlideParameters = playerSlideParameters;
            _playerWallMotionParametersBase = playerWallMotionParametersBase;
            _playerAnimationService = playerAnimationService;
            _playerWaterInHoleParameters = playerWaterInHoleParameters;
        }

        protected override bool GameStateCondition(EGameState gameState) =>
            gameState is EGameState.Game or EGameState.Lose or EGameState.RevivingChoice;

        protected override bool EntityFilter(GameEntity entity) => entity.HasTransform && !entity.IsInterruptPlayerMovement;

        protected override void HandleUpdate(GameEntity entity)
        {
            var blockedMovements = Game.PlayerEntity.BlockedMovements.Value;
            var (playerTransform, playerRotation) = GetPlayerVariables(entity);
            var (forwardSpeed, horizontalSpeed, verticalSpeed) = GetPlayerSpeeds(entity);

            var mainVelocity = Vector3.zero;
            var direction = horizontalSpeed / _playerSpeedService.GetHorizontalSpeed(entity);
            
            HandleForwardSpeed(ref mainVelocity);
            HandleHorizontalSpeed(ref mainVelocity);
            HandleVerticalSpeed(ref mainVelocity);
            HandleAnimationSpeed();
            entity.ReplacePreviousPosition(playerTransform.position);

            if (entity.HasInstantImpulse)
            {
                var oldVelocity = mainVelocity;

                mainVelocity = entity.InstantImpulse.Value;
                mainVelocity += oldVelocity;

                ResetSlideSpeed(entity);

                entity.RemoveInstantImpulse();
            }

            entity.ReplaceVelocity(mainVelocity);
            ClearHorizontalSpeed(entity);

            return;

            void ResetSlideSpeed(GameEntity player)
            {
                if (!player.HasReadyForControllableSlide)
                    return;

                var target = player.ReadyForControllableSlide.StartSpeed;
                var current = target * _playerSlideParameters.ControllableSlideSpeedDecreaseMultiplier;

                player.ReplaceForwardSpeed(new ForwardSpeed(target, current));
            }

            static (Transform playerTransform, Quaternion playerRotation) GetPlayerVariables(GameEntity player)
            {
                var playerTransform = player.Transform.Value;
                var playerRotation = player.Rotation.Value;

                return (playerTransform, playerRotation);
            }

            static (ForwardSpeed forwardSpeed, float horizontalSpeed, float verticalSpeed) GetPlayerSpeeds(GameEntity player)
            {
                var forwardSpeed = player.HasForwardSpeed
                    ? player.ForwardSpeed.Value
                    : new ForwardSpeed(0, 0);

                var horizontalSpeed = player.HasHorizontalSpeed
                    ? player.HorizontalSpeed.Value
                    : 0f;

                var verticalSpeed = player.HasVerticalSpeed
                    ? player.VerticalSpeed.Value
                    : 0f;

                return (forwardSpeed, horizontalSpeed, verticalSpeed);
            }

            void HandleForwardSpeed(ref Vector3 velocity)
            {
                var currentForwardSpeed = 0f;

                if (forwardSpeed.Current >= forwardSpeed.Target)
                {
                    currentForwardSpeed = TryGetForwardSpeedDeceleration(entity, out var forwardSpeedDeceleration)
                        ? Mathf.Lerp(forwardSpeed.Current, forwardSpeed.Target, forwardSpeedDeceleration * _timeProvider.DeltaTime)
                        : forwardSpeed.Target;
                }
                else
                {
                    var acceleration = GetForwardSpeedAcceleration(entity) * _timeProvider.DeltaTime;
                    currentForwardSpeed = math.lerp(forwardSpeed.Current, forwardSpeed.Target, acceleration);
                }

                var moveDirection = playerRotation * Vector3.forward;
                var forwardVelocity = moveDirection * currentForwardSpeed * _timeProvider.DeltaTime;

                forwardSpeed = new ForwardSpeed(forwardSpeed.Target, currentForwardSpeed);
                entity.ReplaceForwardSpeed(forwardSpeed);

                (velocity.x, velocity.z) = (forwardVelocity.x, forwardVelocity.z);
            }

            void HandleHorizontalSpeed(ref Vector3 velocity)
            {
                if (blockedMovements.IsMovementBlocked(EBlockedMovement.Tilting))
                    return;
                
                var playerRotationAngle = playerRotation.eulerAngles.y;

                var additionalVelocity = new Vector2(horizontalSpeed, 0) * _timeProvider.DeltaTime;

                var rotatedAdditionalVector = Quaternion.Euler(0, playerRotationAngle, 0) * additionalVelocity;

                velocity += rotatedAdditionalVector;

                ChangeCameraIfTilting();

                if (entity.BodyMoveType.Value == EBodyMoveType.ControllableSlide)
                    entity.ReplaceControllableSlideDirection(direction);
            }

            void HandleAnimationSpeed()
            {
                if (entity.IsFastActionMove)
                    _playerAnimationService.SetAllAnimationsSpeed(_playerBasicMovementParametersBase.FastMoveRatio);
                else
                    _playerAnimationService.ResetAllAnimationsSpeed();
            }

            void HandleVerticalSpeed(ref Vector3 velocity)
            {
                velocity.y = verticalSpeed * _timeProvider.TimeScale;
            }

            static void ClearHorizontalSpeed(IHorizontalSpeedEntity player)
            {
                player.ReplaceHorizontalSpeed(0f);
            }

            void ChangeCameraIfTilting()
            {
                var threshold = _cameraParameters.HorizontalTiltThreshold;

                var needIgnoreTiltingDuringRun = (entity.IsGhost && !entity.HasGhostRouteData)
                                                 || (entity.IsPlayer && !entity.HasPointerHold);

                switch (entity.BodyMoveType.Value)
                {
                    case EBodyMoveType.Run when needIgnoreTiltingDuringRun:

                    case EBodyMoveType.WallRun:
                        _playerTiltingService.CancelTilting(entity);
                        return;
                }

                if (entity.HasObstaclePassReact)
                    return;
                
                if (!entity.IsFirstPresentationTiltingTutorial) //TODO: Inside the if block, the direction is not checked when horizontalSpeed is 0.
                {
                    if (Mathf.Abs(direction) < threshold)
                        return;
                }

                _playerTiltingService.SetActiveTiltingCamera(entity, direction);
            }
        }

        private float GetForwardSpeedAcceleration(GameEntity player)
        {
            return player.HasBodyMoveType switch
            {
                true when player.BodyMoveType.Value == EBodyMoveType.ControllableSlide =>
                    _playerSlideParameters.ControllableSlideAcceleration,

                true when player.BodyMoveType.Value == EBodyMoveType.WallRun =>
                    _playerWallMotionParametersBase.RunAcceleration,

                _ => _playerBasicMovementParametersBase.ForwardSpeedAcceleration
            };
        }
        
        private bool TryGetForwardSpeedDeceleration(GameEntity player, out float deceleration)
        {
            deceleration = 0f;
            if (!player.IsInWater)
                return false;

            deceleration = _playerWaterInHoleParameters.InWaterForwardSpeedDeceleration;
            return true;
        }
    }
}