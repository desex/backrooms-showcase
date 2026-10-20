using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Movement
{
    [Command]
    public struct ComboAccelerateCommand
    {
        public Uid CharacterUid;
    }
}