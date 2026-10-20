using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class DamageCooldownComponent : IComponent
    {
        public float Value;
    }
}