using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class TargetComponent : IComponent
    {
        public Uid Uid;
    }
}