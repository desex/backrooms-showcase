using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Health
{
    [Command]
    public struct RegenerateAfterDamageCommand
    {
        public Uid CharacterUid;
    }
}