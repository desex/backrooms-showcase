using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class JumpingComponent : IComponent
    {
        public EJumpType Value;
    }
}