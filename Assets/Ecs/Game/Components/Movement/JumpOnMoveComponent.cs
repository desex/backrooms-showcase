using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class JumpOnMoveComponent : IComponent
    {
        public JumpOnMoveParameters Value;
    }
}