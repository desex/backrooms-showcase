using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Lights;
using Game.Utils.Chunks;
using Game.Utils.Lights;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1560, nameof(EFeatures.Chunk))]
    public class EnterTransitionChunkSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly ILightService _lightService;
        private readonly GameContext _game;

        public EnterTransitionChunkSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            ILightService lightService
        ) : base(game)
        {
            _game = game;
            _commandBuffer = commandBuffer;
            _lightService = lightService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CurrentChunkId);
        }

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.IsPlayer)
                return false;

            var currentChunk = _game.GetEntityWithUid(entity.CurrentChunkId.Value);
            return currentChunk.ChunkNode.Value.ChunkType == EChunkType.Transition;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.IsDash)
                    _commandBuffer.Dash(false);
            }
            
            _lightService.Start(ELightEvent.TransitionChunkLightSmooth);
        }
    }
}