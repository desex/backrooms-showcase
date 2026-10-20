using Ecs.Commands.Generator;
using Ecs.Managers;
using Ecs.Utils;

namespace Ecs.Action.Commands
{
    [Command]
    public struct KillPlayerCommand
    {
        public Uid CharacterUid;
        public EDeathReason DeathReason;
        public bool IgnoreHandAppearance;
    }
}