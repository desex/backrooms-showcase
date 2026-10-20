using Game.Utils.QTE;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.QTE
{
    [Game]
    [Event(EventTarget.Self)]
    public class QteResultComponent : IComponent
    {
        public EQteResult Value;
    }
}