using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands
{
    [Command]
    public struct JumpCommand
    {
        public Uid CharacterUid;
        public float Height;
    }
}