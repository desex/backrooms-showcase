using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands
{
    [Command]
    public struct SetPlayerInvulnerableCommand
    {
        public Uid CharacterUid;
    }
}