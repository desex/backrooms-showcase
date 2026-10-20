using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class VerticalSpeedComponent : IComponent
    {
        public float Value;
    }
}