using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Unique]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class PauseComponent : IComponent
    {
        public bool Value;
    }
}