using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    [Event(EventTarget.Self)]
    public class MovementStateComponent : IComponent
    {
        public EMovementState Value;
    }
}