using Ecs.Core.Systems;
using Ecs.Utils;

namespace Ecs.Game.Systems
{
    public abstract class APausableGameStateConditionUpdateSystem : APausableConditionUpdateSystem
    {
        protected APausableGameStateConditionUpdateSystem(GameContext game) : base(game)
        {
        }

        protected abstract bool GameStateCondition(EGameState gameState);

        protected override bool ConditionFilter()
        {
            return Game.GameStateEntity != null
                   && GameStateCondition(Game.GameStateEntity.GameState.Value)
                   && base.ConditionFilter();
        }
    }
}