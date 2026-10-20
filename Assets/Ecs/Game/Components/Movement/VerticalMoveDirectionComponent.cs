using Ecs.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class VerticalMoveDirectionComponent : IComponent
    {
        public EVerticalMoveDirection Value;
    }
}