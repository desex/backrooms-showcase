using Game.Utils.Lights;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Light
{
    [Game]
    [Event(EventTarget.Self)]
    public class LightEventTypeComponent : IComponent
    {
        public ELightEvent Value;
    }
}