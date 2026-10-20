using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Level
{
    [Game]
    [Event(EventTarget.Self)]
    public class LastTransitionDistanceComponent : IComponent
    {
        public int Value;
    }
}