using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Ghosts
{
    [Command]
    public struct RandomNewGhostChancesCommand
    {
        public Uid GhostUid;
    }
}