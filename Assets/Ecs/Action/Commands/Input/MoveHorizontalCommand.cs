using Ecs.Commands.Generator;

namespace Ecs.Action.Commands.Input
{
    [Command]
    public struct MoveHorizontalCommand
    {
        public float NormalizedDelta;
    }
}