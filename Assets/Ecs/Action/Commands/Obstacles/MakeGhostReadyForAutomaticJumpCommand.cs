using Ecs.Commands.Generator;
using Ecs.Managers;
using Game.Utils.Ghost;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct MakeGhostReadyForAutomaticJumpCommand
    {
        public Uid GhostUid;
        public EGhostSimpleJumpType JumpType;
    }
}