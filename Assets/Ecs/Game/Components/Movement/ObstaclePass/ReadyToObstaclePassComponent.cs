using Ecs.Utils.ObstaclePass;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement.ObstaclePass
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class ReadyToObstaclePassComponent : IComponent
    {
        public ReadyToObstaclePassData Value;
    }
}