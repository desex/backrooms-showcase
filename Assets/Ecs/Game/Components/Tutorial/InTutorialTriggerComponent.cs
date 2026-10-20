using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Tutorial
{
    [Game]
    [Event(EventTarget.Self)]
    public class InTutorialTriggerComponent : IComponent
    {
        public Uid Value;
    }
}