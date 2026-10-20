using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 666, nameof(EFeatures.Move))]
    public class SlideDurationUpdateSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly ITimeProvider _timeProvider;

        public SlideDurationUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            ICommandBuffer commandBuffer
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _commandBuffer = commandBuffer;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasSlideDuration && !entity.IsDead && !entity.IsDestroyed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var slideTime = entity.SlideDuration.Value;

            slideTime -= _timeProvider.DeltaTime;

            if (slideTime <= 0f || entity.IsReadyToFailFall || entity.IsFallInterrupted)
            {
                if (entity.HasReadyForControllableSlide)
                    return;

                _commandBuffer.CancelSlide(entity.Uid.Value);
                entity.ReplaceSwipeDownCount(0);

                return;
            }

            entity.ReplaceSlideDuration(slideTime);
        }
    }
}