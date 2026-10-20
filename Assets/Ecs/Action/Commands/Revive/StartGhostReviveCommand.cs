using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Revive
{
    [Command]
    public struct StartGhostReviveCommand
    {
        public Uid CharacterUid;
    }
}