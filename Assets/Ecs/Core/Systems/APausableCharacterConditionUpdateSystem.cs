using Ecs.Utils.Groups;

namespace Ecs.Core.Systems
{
    public abstract class APausableCharacterConditionUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        protected APausableCharacterConditionUpdateSystem(
            GameContext game, 
            IGameGroupUtils gameGroupUtils
        ) : base(game)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        protected abstract bool EntityFilter(GameEntity entity);
        protected abstract void HandleUpdate(GameEntity entity);

        protected sealed override void UpdateSystem()
        {
            using var _ = _gameGroupUtils.GetCharacters(out var characters);
            
            foreach (var gameEntity in characters)
            {
                if (!EntityFilter(gameEntity))
                    continue;
                
                HandleUpdate(gameEntity);
            }
        }
    }
}