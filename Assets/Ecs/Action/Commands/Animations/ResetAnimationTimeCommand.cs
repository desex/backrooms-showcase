using Ecs.Commands.Generator;

namespace Ecs.Action.Commands.Animations
{
    [Command]
    public struct ResetAnimationTimeCommand
    {
        public int Layer;
    }
}