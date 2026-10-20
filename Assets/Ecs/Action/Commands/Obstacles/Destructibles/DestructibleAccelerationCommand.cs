using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Obstacles.Destructibles
{
    [Command]
    public struct DestructibleAccelerationCommand
    {
        public Uid CharacterUid;
    }
}