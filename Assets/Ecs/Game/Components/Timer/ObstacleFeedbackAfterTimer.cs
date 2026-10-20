using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Timer
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class ObstacleFeedbackAfterTimer : IComponent
    {
        public float Value;
    }
}