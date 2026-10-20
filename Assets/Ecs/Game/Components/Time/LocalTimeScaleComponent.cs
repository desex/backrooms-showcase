using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Time
{
    [Game]
    [Event(EventTarget.Self)]
    public class LocalTimeScaleComponent : IComponent
    {
        public float Value;
    }
}