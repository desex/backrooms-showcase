using Game.Utils.Movement;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement
{
    [Game]
    public class ReadyToJumpOnComponent : IComponent
    {
        public JumpOnParameters Value;
    }
}