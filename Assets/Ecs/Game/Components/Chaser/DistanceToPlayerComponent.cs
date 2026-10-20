using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Chaser
{
    [Game]
    [Event(EventTarget.Self)]
    public class DistanceToPlayerComponent : IComponent
    {
        public float Value;
    }
}