using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;

namespace Ecs.Game.Systems
{
    public abstract class APausableCharacterGameStateConditionUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        protected APausableCharacterGameStateConditionUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        protected abstract bool GameStateCondition(EGameState gameState);
        protected abstract bool EntityFilter(GameEntity entity);
        protected abstract void HandleUpdate(GameEntity entity);

        protected override bool ConditionFilter()
        {
            // Более показать не могу
            return default;
        }

        protected sealed override void UpdateSystem()
        {
            // Более показать не могу
        }
    }
}