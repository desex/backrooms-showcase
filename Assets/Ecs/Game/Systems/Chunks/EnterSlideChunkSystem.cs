using System.Collections.Generic;
using Db.Player;
using Ecs.Utils;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1561, nameof(EFeatures.Chunk))]
    public class EnterSlideChunkSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        
        public EnterSlideChunkSystem(
            IContext<GameEntity> context,
            ICommandBuffer commandBuffer, 
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase
        ) : base(context)
        {
            _commandBuffer = commandBuffer;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ReadyForControllableSlide.Added());
        }
        
        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer && !entity.IsDestroyed && entity.HasSlideChunk;
        }
        
        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.IsDash)
                    _commandBuffer.Dash(false);
                
                var baseSpeed = _playerBasicMovementParametersBase.Speed;
                entity.ReplaceForwardSpeed(new ForwardSpeed(baseSpeed, baseSpeed));
            }
        }
    }
}