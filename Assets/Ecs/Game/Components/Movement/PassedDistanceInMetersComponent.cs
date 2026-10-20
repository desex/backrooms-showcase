using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class PassedDistanceInMetersComponent : IComponent
    {
        public int Value;
    }
}