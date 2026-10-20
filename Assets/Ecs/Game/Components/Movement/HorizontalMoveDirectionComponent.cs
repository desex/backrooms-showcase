using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class HorizontalMoveDirectionComponent : IComponent
    {
        public EHorizontalMoveDirection Value;
    }
}