using Game.Utils.MovementBlock;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Tutorial
{
    [Game]
    public class BlockedMovementsComponent : IComponent
    {
        public EBlockedMovement Value;
    }
}