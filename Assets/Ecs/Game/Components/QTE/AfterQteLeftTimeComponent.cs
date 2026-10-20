using JCMG.EntitasRedux;

namespace Ecs.Game.Components.QTE
{
    [Game]
    [Event(EventTarget.Self)]
    public class AfterQteLeftTimeComponent : IComponent
    {
        public float Value;
    }
}