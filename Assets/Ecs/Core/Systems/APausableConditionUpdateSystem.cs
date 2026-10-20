namespace Ecs.Core.Systems
{
    public abstract class APausableConditionUpdateSystem : AConditionUpdateSystem
    {
        protected readonly GameContext Game;

        protected APausableConditionUpdateSystem(GameContext game)
        {
            Game = game;
        }

        protected virtual bool Filter() => true;

        protected override bool ConditionFilter() => 
            Game.PauseEntity.Pause.Value == false && Filter();
    }
}