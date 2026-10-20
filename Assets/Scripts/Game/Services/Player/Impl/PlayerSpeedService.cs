using Db.Obstacles;
using Db.Player;
using Game.Services.Settings;
using Game.Utils.Movement;

namespace Game.Services.Player.Impl
{
    public sealed class PlayerSpeedService : IPlayerSpeedService
    {
        private readonly IAccelerationFactorService _accelerationFactorService;
        private readonly GameContext _game;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerForwardJumpParametersBase _playerForwardJumpParametersBase;
        private readonly IPlayerSlideAndJumpParameters _playerSlideAndJumpParameters;
        private readonly IPlayerWallMotionParametersBase _playerWallMotionParameters;
        private readonly IPlayerWaterInHoleParametersBase _playerWaterInHoleParameters;
        private readonly ISettingsService _settingsService;
        private readonly ITravelatorParameters _travelatorParameters;

        private float _horizontalSensitivityMultiply = 1f;

        public PlayerSpeedService(
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSlideAndJumpParameters playerSlideAndJumpAndJumpParameters,
            IPlayerForwardJumpParametersBase playerForwardJumpParametersBase,
            IPlayerWallMotionParametersBase playerWallMotionParameters,
            ISettingsService settingsService,
            GameContext game,
            IAccelerationFactorService accelerationFactorService,
            IPlayerWaterInHoleParametersBase playerWaterInHoleParameters
        )
        {
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSlideAndJumpParameters = playerSlideAndJumpAndJumpParameters;
            _playerForwardJumpParametersBase = playerForwardJumpParametersBase;
            _playerWallMotionParameters = playerWallMotionParameters;
            _settingsService = settingsService;
            _game = game;
            _accelerationFactorService = accelerationFactorService;
            _playerWaterInHoleParameters = playerWaterInHoleParameters;
        }

        public float GetForwardSpeed(GameEntity entity)
        {
            if (entity.IsDead)
                return 0;

            var tutorialEntity = _game.TutorialEntity;
            var isTutorialActive = tutorialEntity.IsInTutorial;
            var speed = 0f;
            
            if (entity.HasPlayerOnTravelatorParameters)
            {
                var modifier = entity.PlayerOnTravelatorParameters.Value.PlayerSpeedAdjustmentFactor;
                
                var percentSpeed = 1f + modifier;
                var newSpeed = entity.PlayerOnTravelatorParameters.Value.StartForwardSpeed * percentSpeed;
                
                speed = newSpeed;
                
                return speed;
            }
            
            speed = GetBasicForwardSpeed(entity) * entity.BerserkSpeedMultiplier.Value;
            
            if (isTutorialActive)
                return speed;

            if (entity.IsDash)
                return speed * entity.ForwardSpeedMultiplier.Value;

            speed *= entity.ForwardSpeedMultiplier.Value * entity.DestructibleSpeedMultiplier.Value;

            if (!entity.HasJumping || entity.Jumping.Value is not EJumpType.JumpOverObstacle)
                AccelerateSpeedIfFastAction(entity, ref speed);

            if (!IsPlayerJumpingOrSliding(entity))
                ClearMoveAccelerateSpeed(entity, ref speed);

            if (entity.HasSlowDownRatio)
                speed *= 1 - entity.SlowDownRatio.Value;

            if (entity.HasReadyForControllableSlide)
            {
                float maxTargetForwardSpeed;

                if (entity.HasJumping)
                {
                    if (entity.Jumping.Value == EJumpType.Jump)
                    {
                        maxTargetForwardSpeed = GetMaxTargetForwardSpeed(entity) * .8f; //TODO вынести в конфиги
                    }
                    else if (entity.Jumping.Value == EJumpType.DoubleJump)
                    {
                        maxTargetForwardSpeed = GetMaxTargetForwardSpeed(entity) * .9f; //TODO вынести в конфиги
                    }
                    else
                    {
                        maxTargetForwardSpeed = GetMaxTargetForwardSpeed(entity); //TODO такой кейф вообще маловероятен
                    }
                }
                else
                {
                    maxTargetForwardSpeed = GetMaxTargetForwardSpeed(entity) * 2.2f; //TODO вынести в конфиги
                }

                return maxTargetForwardSpeed;
            }

            if (entity.IsInWater)
            {
                var minSpeedThreshold = _playerWaterInHoleParameters.InWaterForwardSpeed;
                speed = minSpeedThreshold;
            }

            return speed;
        }

        public float GetMaxTargetForwardSpeed(GameEntity player)
        {
            var speed = _playerBasicMovementParametersBase.Speed * player.ForwardSpeedMultiplier.Value;
            return speed;
        }

        public void ChangeHorizontalSensitivity(float multiply = 1f)
        {
            _horizontalSensitivityMultiply = multiply;
        }

        public float GetHorizontalSpeed(GameEntity player)
        {
            var currentSensitivity = _settingsService.Sensitivity.CurrentValue * _horizontalSensitivityMultiply;
           
            if (player.HasSliding)
                return _playerSlideAndJumpParameters.ControllableSlideHorizontalSpeed * currentSensitivity;

            if (player.IsDamaged)
                return _playerBasicMovementParametersBase.InjuredHorizontalSpeed * currentSensitivity;

            var horizontalMoveSpeed = _playerBasicMovementParametersBase.HorizontalSpeed;

            if (player.HasJumping && player.Jumping.Value is EJumpType.Jump or EJumpType.DoubleJump)
            {
                float jumpHorizontalSpeedRatio;

                if (player.HasSlideChunk)
                {
                    jumpHorizontalSpeedRatio = GetMaxTargetForwardSpeed(player) * .8f; //TODO в конфиг
                }
                else
                {
                    jumpHorizontalSpeedRatio = _playerBasicMovementParametersBase.JumpHorizontalSpeedRatio
                                               * horizontalMoveSpeed
                                               * currentSensitivity;
                }

                return jumpHorizontalSpeedRatio;
            }

            return horizontalMoveSpeed * currentSensitivity;
        }

        public float GetBasicForwardSpeed(GameEntity player)
        {
            var isControlledSliding = player.HasSliding
                                      && player.HasBodyMoveType
                                      && player.BodyMoveType.Value == EBodyMoveType.ControllableSlide;

            if (isControlledSliding)
                return _playerSlideAndJumpParameters.ControllableSlideSpeed;

            var isInDoubleJump = player.HasJumping && player.Jumping.Value == EJumpType.DoubleJump;

            if (isInDoubleJump)
            {
                return player.HasSlideChunk
                    ? _playerSlideAndJumpParameters.ControllableSlideSpeed / 2
                    : _playerSlideAndJumpParameters.DoubleJumpSpeed;
            }

            var hasBodyTypeSpeed = TryGetForwardSpeedForBodyType(player, out var bodyTypeSpeed);

            if (hasBodyTypeSpeed)
                return bodyTypeSpeed;

            if (player.IsDamaged)
                return _playerBasicMovementParametersBase.InjuredSpeed;

            if (player.IsMovingUpSurface)
                return _playerBasicMovementParametersBase.UpSurfaceSpeed;

            return _playerBasicMovementParametersBase.Speed;
        }

        private void AccelerateSpeedIfFastAction(GameEntity player, ref float speed)
        {
            if (!player.IsFastActionMove || player.IsDamaged)
                return;

            var fastMoveRatio = _playerBasicMovementParametersBase.FastMoveRatio;
            speed *= fastMoveRatio;
        }

        private void ClearMoveAccelerateSpeed(GameEntity player, ref float speed)
        {
            var isAccelerate = player.HasSpeedAcceleration;

            if (!isAccelerate)
                return;

            if (player.IsPlayerOnTravelator)
                return;

            speed *= _accelerationFactorService.GetAccelerationFactor();
        }

        private bool TryGetForwardSpeedForBodyType(GameEntity player, out float forwardSpeed)
        {
            forwardSpeed = 0;

            if (!player.HasBodyMoveType) return false;

            switch (player.BodyMoveType.Value)
            {
                case EBodyMoveType.ControllableSlide when player.HasSliding:
                    forwardSpeed = _playerSlideAndJumpParameters.ControllableSlideSpeed;
                    return true;

                case EBodyMoveType.ForwardJump:
                    forwardSpeed = _playerForwardJumpParametersBase.Speed;
                    return true;

                case EBodyMoveType.WallRun:
                    forwardSpeed = _playerWallMotionParameters.Speed;
                    return true;
            }

            return false;
        }

        private static bool IsPlayerJumpingOrSliding(GameEntity player)
        {
            return player.HasSliding && player.Sliding.Type is ESlideType.Short
                   || player.HasJumping && player.Jumping.Value is EJumpType.Jump or EJumpType.JumpOverObstacle;
        }
    }
}