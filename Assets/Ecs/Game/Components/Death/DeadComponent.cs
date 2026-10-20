using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Death
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class DeadComponent : IComponent
    {
        public override string ToString()
        {
            // Более показать не могу
            return default;
        }
    }
}