using Game.Utils.QTE;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.QTE
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class QteTypeComponent : IComponent
    {
        public EQteType Value;
    }
}