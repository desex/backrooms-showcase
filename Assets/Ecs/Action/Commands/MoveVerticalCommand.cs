using Ecs.Commands.Generator;

namespace Ecs.Action.Commands
{
    [Command]
    public struct MoveVerticalCommand
    {
        public float NormalizedDelta;
    }
}