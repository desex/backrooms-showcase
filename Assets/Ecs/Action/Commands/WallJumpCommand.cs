using Ecs.Commands.Generator;

namespace Ecs.Action.Commands
{
    [Command]
    public struct WallJumpCommand
    {
        public float Direction;
    }
}