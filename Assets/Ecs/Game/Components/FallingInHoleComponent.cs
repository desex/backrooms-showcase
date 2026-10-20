using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self, EventType.Removed)]
    public class FallingInHoleComponent : IComponent
    {
    }
}