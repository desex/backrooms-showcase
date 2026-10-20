#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Ai;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Ai
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1570, nameof(EFeatures.Ai))]
    public class AiPauseSystem : ReactiveSystem<GameEntity>
    {
        private readonly IBehaviourTreesService _behaviourTreesService;
        private readonly IGameGroupUtils _gameGroupUtils;

        public AiPauseSystem(
            GameContext game,
            IBehaviourTreesService behaviourTreesService,
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _behaviourTreesService = behaviourTreesService;
            _gameGroupUtils = gameGroupUtils;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Pause);

        protected override bool Filter(GameEntity entity) => entity.HasPause && !entity.IsDestroyed; 

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var pauseEntity in entities)
            {
                using var _ = _gameGroupUtils.GetAi(out var allAi, e => e.IsAiEnabled);
                foreach (var ai in allAi)
                {
                    var bTree = ai.BTree.Value;
                    _behaviourTreesService.SetBehaviorPause(bTree, pauseEntity.Pause.Value);
                }
            }
        }
    }
}
#endif