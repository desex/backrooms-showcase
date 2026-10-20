using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class TurningComponent : IComponent
    {
        public TurningValues Value;
    }
}