using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Tutorial
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class InTutorialComponent : IComponent
    {
    }
}