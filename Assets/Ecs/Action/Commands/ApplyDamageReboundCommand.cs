using Ecs.Commands.Generator;
using Ecs.Managers;
using Game.Utils.Rebound;

namespace Ecs.Action.Commands
{
    [Command]
    public struct ApplyDamageReboundCommand
    {
        public Uid CharacterUid;
        public ReboundSetup ReboundSetup;
    }
}