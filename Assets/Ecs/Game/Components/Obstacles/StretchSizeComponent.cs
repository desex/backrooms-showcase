using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Obstacles
{
    [Game]
    [Event(EventTarget.Self)]
    public class StretchSizeComponent : IComponent
    {
        public float Value;
    }
}