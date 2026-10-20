using System.Collections.Generic;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 938, nameof(EFeatures.Move))]
    public class ResetPlayerExtraGravitySystem : ReactiveSystem<GameEntity>
    {
        public ResetPlayerExtraGravitySystem(
            IContext<GameEntity> context
        ) : base(context)
        {
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UseExtraGravity.Removed());
        }
        
        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer & !entity.IsDestroyed & !entity.HasJumping;
        }
        
        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceVerticalSpeed(0f);
            }
        }
    }
}