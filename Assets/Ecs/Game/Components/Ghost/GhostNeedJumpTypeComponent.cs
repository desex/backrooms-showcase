using Game.Utils.Ghost;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Ghost
{
    [Game]
    public class GhostNeedJumpTypeComponent : IComponent
    {
        public EGhostSimpleJumpType Value;
    }
}