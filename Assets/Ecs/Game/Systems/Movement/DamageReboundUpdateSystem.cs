using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Buffer;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 951, nameof(EFeatures.Move))]
    public class DamageReboundUpdateSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly IReboundParameters _reboundParameters;
        private readonly ITimeProvider _timeProvider;
        private readonly IBufferSwipeService _bufferSwipeService;

        public DamageReboundUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            IReboundParameters reboundParameters,
            IBufferSwipeService bufferSwipeService
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _reboundParameters = reboundParameters;
            _bufferSwipeService = bufferSwipeService;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasDamageReboundDirection;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var time = entity.HasDamageReboundTime
                ? entity.DamageReboundTime.Value
                : 0f;

            time += _timeProvider.DeltaTime;
            entity.ReplaceDamageReboundTime(time);

            if (time >= _reboundParameters.ReboundDuration || InAnyMotion(entity))
            {
                entity.RemoveDamageReboundDirection();
                entity.RemoveDamageReboundDirectionType();
                entity.RemoveDamageReboundTime();
                entity.CharacterController.Value.enabled = true;
                entity.IsEnableCharacterTrigger = true;
                entity.IsReboundState = true;
                
                entity.IsCanSwipeVertical = true;
                _bufferSwipeService.CallBufferSwipe(entity);
                
                return;
            }

            var timeParameter = time / _reboundParameters.ReboundDuration;

            var impulse = entity.HasReadyForControllableSlide
                ? _reboundParameters.SlideReboundImpulse
                : _reboundParameters.ReboundImpulse;
            
            var targetImpulse = _reboundParameters.ReboundCurve.Evaluate(timeParameter) * impulse;
            var direction = entity.DamageReboundDirection.Value;

            entity.ReplaceInstantImpulse(direction * targetImpulse * _timeProvider.DeltaTime);
        }

        private static bool InAnyMotion(GameEntity player)
        {
            var bodyMoveType = player.BodyMoveType.Value;

            return bodyMoveType is EBodyMoveType.ForwardJump
                or EBodyMoveType.JumpOn
                or EBodyMoveType.Climb
                or EBodyMoveType.Roll
                or EBodyMoveType.WallRun
                or EBodyMoveType.LongSlide;
        }
    }
}