using Game.Utils.Obstacles;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Obstacles
{
    [Game]
    [Event(EventTarget.Self)]
    public class ObstacleMovePointReachedComponent : IComponent
    {
        public MovePointReachedData Value;
    }
}