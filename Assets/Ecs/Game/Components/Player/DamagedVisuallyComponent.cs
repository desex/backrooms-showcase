using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class DamagedVisuallyComponent : IComponent
    {
    }
}