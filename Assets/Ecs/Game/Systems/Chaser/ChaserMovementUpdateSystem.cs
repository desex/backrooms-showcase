using Db.ChasePlayer;
using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Movement;
using Game.Utils.QTE;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Chaser
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Chaser))]
    public class ChaserMovementUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly GameContext _gameContext;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerSlideAndJumpParameters _playerSlideParameters;
        private readonly IPlayerSpeedService _playerSpeedService;

        private readonly ITimeProvider _timeProvider;

        public ChaserMovementUpdateSystem(
            GameContext game,
            ITimeProvider timeProvider,
            IChasePlayerParameters chasePlayerParameters,
            IPlayerSpeedService playerSpeedService,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase,
            IPlayerSlideAndJumpParameters playerSlideParameters
        ) : base(game)
        {
            _timeProvider = timeProvider;
            _chasePlayerParameters = chasePlayerParameters;
            _gameContext = game;
            _playerSpeedService = playerSpeedService;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
            _playerSlideParameters = playerSlideParameters;
        }

        protected override bool Filter()
        {
            var player = Game.PlayerEntity;
            var tutorialEntity = Game.TutorialEntity;
            var isInTutorial = tutorialEntity.IsInTutorial;

            return !isInTutorial
                && !player.IsDead
                && !player.IsDestroyed
                && !player.IsEnteredTransitionHole
                && player.HasForwardSpeed
                && Game.GameState.Value == EGameState.Game;
        }

        protected override void UpdateSystem()
        {
            var chaser = Game.ChaserEntity;
            var player = Game.PlayerEntity;

            if (player.HasQteType && player.QteType.Value == EQteType.Chaser)
            {
                chaser.ReplaceDistanceToPlayer(0f);
                return;
            }

            var playerSpeed = player.ForwardSpeed.Value.Current;
            var deltaTime = _timeProvider.DeltaTime;

            var newChaserSpeed = UpdateCurrentSpeed();
            chaser.ReplaceForwardSpeed(newChaserSpeed);

            var shiftDistance = (chaser.ForwardSpeed.Value.Current - playerSpeed) * deltaTime;
                
            if (TryConsumeProgressShift(out var shift))
                shiftDistance += shift;

            var newDistance = chaser.DistanceToPlayer.Value - (chaser.IsInAfterQteSlowdown ? -shiftDistance : shiftDistance);
            
            var clamped = Mathf.Clamp(newDistance, 0f, _chasePlayerParameters.MaxDistance);
            chaser.ReplaceDistanceToPlayer(clamped);
        }

        public ForwardSpeed UpdateCurrentSpeed()
        {
            var chaser = _gameContext.ChaserEntity;
            var player = _gameContext.PlayerEntity;

            UpdateShift(player, chaser);

            var chaserTargetSpeed = ChaserTargetSpeed(player, chaser, out var currentForwardSpeed);

            return new ForwardSpeed(chaserTargetSpeed, currentForwardSpeed);
        }

        public bool TryConsumeProgressShift(out float shift)
        {
            var chaser = Game.ChaserEntity;

            if (!chaser.HasRemainingProgressShift)
            {
                shift = 0;
                return false;
            }

            var remainingProgressShift = chaser.RemainingProgressShift.Value;

            if (remainingProgressShift <= 0)
            {
                shift = 0;
                chaser.RemoveRemainingProgressShift();
                return false;
            }
            
            shift = UpdateRemainingShift(remainingProgressShift, chaser);

            return true;
        }

        private float UpdateRemainingShift(float remainingProgressShift, GameEntity chaser)
        {
            var config = _chasePlayerParameters;
            var maxDistance = config.MaxDistance;
            var shiftPerSecond = maxDistance * config.PlayerInjuredBarProgression / config.SingleSegmentDuration;
            var shift = shiftPerSecond * _timeProvider.DeltaTime;

            shift = Mathf.Min(shift, remainingProgressShift);
            
            chaser.ReplaceRemainingProgressShift(remainingProgressShift - shift);

            return shift;
        }

        private void UpdateShift(GameEntity player, GameEntity chaser)
        {
            if (chaser.IsInAfterQteSlowdown && !player.IsAfterQteLeftHandler)
            {
                player.IsAfterQteLeftHandler = true;
                ReplaceRemainingShift();
            }
            else if (!chaser.IsInAfterQteSlowdown && player.IsAfterQteLeftHandler)
            {
                player.IsAfterQteLeftHandler = false;
            }

            if (player.IsDamaged && !player.IsDamageHandled)
            {
                player.IsDamageHandled = true;
                ReplaceRemainingShift();
            }
            else if (!player.IsDamaged)
            {
                player.IsDamageHandled = false;
            }
            
            return;
            
            void ReplaceRemainingShift()
            {
                var config = _chasePlayerParameters;
                var value = chaser.HasRemainingProgressShift ? chaser.RemainingProgressShift.Value : 0f;

                chaser.ReplaceRemainingProgressShift(value + config.MaxDistance * config.PlayerInjuredBarProgression);
            }
        }

        private float ChaserTargetSpeed(GameEntity player, GameEntity chaser, out float currentForwardSpeed)
        {
            if (!chaser.IsCanAccelerate)
            {
                var playerForwardSpeed = player.ForwardSpeed.Value;
                currentForwardSpeed = playerForwardSpeed.Current;

                return playerForwardSpeed.Current;
            }

            var chaserTargetSpeed =
                _playerSpeedService.GetBasicForwardSpeed(player) * player.ForwardSpeedMultiplier.Value;
            
            if (chaser.HasChaserSpeedAdjustmentFactor)
            {
                var modifier = chaser.ChaserSpeedAdjustmentFactor.Value;
                
                var percentSpeed = 1f + modifier;
                var newSpeed = chaserTargetSpeed * percentSpeed;
                
                chaserTargetSpeed = newSpeed;
            }

            var chaserForwardSpeed = chaser.ForwardSpeed.Value;
            var acceleration = GetForwardSpeedAcceleration(player) * _timeProvider.DeltaTime;

            currentForwardSpeed = chaserForwardSpeed.Current >= chaserForwardSpeed.Target
                ? chaserForwardSpeed.Target
                : Mathf.Lerp(chaserForwardSpeed.Current, chaserTargetSpeed, acceleration);

            return chaserTargetSpeed;
        }

        private float GetForwardSpeedAcceleration(GameEntity player)
        {
            return player.HasBodyMoveType switch
            {
                true when player.BodyMoveType.Value == EBodyMoveType.ControllableSlide =>
                    _playerSlideParameters.ControllableSlideAcceleration,

                _ => _playerBasicMovementParametersBase.ForwardSpeedAcceleration,
            };
        }
    }
}