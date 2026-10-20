using Ecs.Commands.Generator;

namespace Ecs.Action.Commands
{
    [Command]
    public struct SetPlayerCanBeDamagedCommand
    {
        public bool IsCanDamaged;
    }
}