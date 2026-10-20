#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Ai
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1505, nameof(EFeatures.Ai))]
    public class AiGameStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;

        public AiGameStateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _game = game;
            _gameGroupUtils = gameGroupUtils;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.GameState);

        protected override bool Filter(GameEntity entity)
            => entity.HasGameState && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            var gameState = _game.GameState.Value;

            if (gameState is not (EGameState.Game or EGameState.Lose))
                return;

            var needEnable = gameState == EGameState.Game;
            
            using var _ = _gameGroupUtils.GetAi(out var buffer, e => e.HasAiType);

            foreach (var entity in buffer)
                entity.IsAiEnabled = needEnable;
        }
    }
}
#endif