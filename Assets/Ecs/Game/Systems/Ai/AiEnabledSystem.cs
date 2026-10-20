#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Ai;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Ai
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1569, nameof(EFeatures.Ai))]
    public class AiEnabledSystem : ReactiveSystem<GameEntity>
    {
        private readonly IBehaviourTreesService _behaviourTreesService;

        public AiEnabledSystem(
            GameContext game,
            IBehaviourTreesService behaviourTreesService
        ) : base(game)
        {
            _behaviourTreesService = behaviourTreesService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.AiEnabled);

        protected override bool Filter(GameEntity entity) => entity.IsAi && entity.HasBTree && !entity.IsDestroyed; 

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var bTree = entity.BTree.Value;
                
                if (entity.IsAiEnabled)
                    _behaviourTreesService.EnableAndResetBehavior(bTree);
                else
                    _behaviourTreesService.DisableBehavior(bTree);
            }
        }
    }
}
#endif