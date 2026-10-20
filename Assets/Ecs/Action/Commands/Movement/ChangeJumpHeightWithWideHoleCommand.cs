using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Movement
{
    [Command]
    public struct ChangeJumpHeightWithWideHoleCommand
    {
        public Uid CharacterUid;
        public bool IsEntering;
    }
}