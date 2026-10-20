using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class ControllableSlideDirectionComponent : IComponent
    {
        public float Value;
    }
}