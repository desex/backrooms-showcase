using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 900, nameof(EFeatures.Move))]
    public class PlankRunCentralizeUpdateSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly IPlayerPlankRunParametersBase _playerPlankRunParametersBase;
        private readonly ITimeProvider _timeProvider;

        public PlankRunCentralizeUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            IPlayerPlankRunParametersBase playerPlankRunParametersBase
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _playerPlankRunParametersBase = playerPlankRunParametersBase;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return !entity.IsDead && !entity.IsInterruptPlayerMovement && entity.HasPlankRun && !entity.IsDestroyed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var startPosition = entity.PlankRun.Value.StartPoint;
            var playerPosition = entity.PlankRun.Value.PlayerPosition;

            var duration = entity.HasPlankRunCentralizeDuration ? entity.PlankRunCentralizeDuration.Value : 0f;
            var centralizeTime = _playerPlankRunParametersBase.CentralizeDuration;

            if (duration >= centralizeTime)
            {
                entity.ReplacePosition(startPosition);
                entity.RemovePlankRunCentralizeDuration();

                entity.CharacterController.Value.enabled = true;
                entity.IsEnableCharacterTrigger = true;

                return;
            }

            var newPosition = playerPosition + (startPosition - playerPosition) * (duration / centralizeTime);
            duration += _timeProvider.DeltaTime;

            entity.ReplacePosition(newPosition);
            entity.ReplacePlankRunCentralizeDuration(duration);
        }
    }
}