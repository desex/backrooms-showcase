using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class PreviousMovementStateComponent : IComponent
    {
        public EMovementState Value;
    }
}