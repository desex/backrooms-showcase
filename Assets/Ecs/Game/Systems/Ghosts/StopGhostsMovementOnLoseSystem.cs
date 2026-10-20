#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Ghosts
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1506, nameof(EFeatures.Ai))]
    public class StopGhostsMovementOnLoseSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public StopGhostsMovementOnLoseSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.GameState);

        protected override bool Filter(GameEntity entity)
            => entity.HasGameState && entity.GameState.Value == EGameState.Lose && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            using var _ = _gameGroupUtils.GetGhosts(out var buffer);

            foreach (var entity in buffer)
                entity.IsInterruptPlayerMovement = true;
        }
    }
}
#endif