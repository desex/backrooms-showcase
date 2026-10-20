using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Action.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 950, nameof(EFeatures.Common))]
    public class DamageCooldownSystem : ACharacterUpdateSystem
    {
        private readonly ITimeProvider _timeProvider;

        public DamageCooldownSystem(IGameGroupUtils gameGroupUtils, ITimeProvider timeProvider) : base(gameGroupUtils)
        {
            _timeProvider = timeProvider;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasDamageCooldown && !entity.IsDead && !entity.IsDestroyed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var cooldown = entity.DamageCooldown.Value;

            cooldown -= _timeProvider.DeltaTime;

            if (cooldown <= 0)
            {
                entity.RemoveDamageCooldown();
                return;
            }

            entity.ReplaceDamageCooldown(cooldown);
        }
    }
}