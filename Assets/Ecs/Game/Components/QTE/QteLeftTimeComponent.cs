using JCMG.EntitasRedux;

namespace Ecs.Game.Components.QTE
{
    [Game]
    [Event(EventTarget.Self)]
    public class QteLeftTimeComponent : IComponent
    {
        public float Value;
    }
}