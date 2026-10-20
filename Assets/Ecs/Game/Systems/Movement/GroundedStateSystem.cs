using System.Collections.Generic;
using Ecs.Utils;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 939, nameof(EFeatures.Move))]
    public class PlayerGroundCheckSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICommandBuffer _commandBuffer;
        
        public PlayerGroundCheckSystem(
            IContext<GameEntity> context,
            ICommandBuffer commandBuffer
        ) : base(context)
        {
            _commandBuffer = commandBuffer;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Grounded.Added());
        }
        
        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer & !entity.IsDestroyed;
        }
        
        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            _commandBuffer.Grounded();
        }
    }
}