using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands
{
    [Command]
    public struct InterruptControllableSlideCommand
    {
        public Uid CharacterUid;
    }
}