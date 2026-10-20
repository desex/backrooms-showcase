using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class PassedDistanceComponent : IComponent
    {
        public float Value;
    }
}