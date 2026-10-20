using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Timer
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class BerserkModeTimerComponent : IComponent
    {
        public float Value;
    }
}