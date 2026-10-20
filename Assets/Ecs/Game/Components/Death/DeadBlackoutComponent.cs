using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Death
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class DeadBlackoutComponent : IComponent
    {
        public BlackoutSetups Value;
    }
}