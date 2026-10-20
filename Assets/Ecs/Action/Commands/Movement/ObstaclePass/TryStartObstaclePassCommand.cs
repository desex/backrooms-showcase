using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Movement.ObstaclePass
{
    [Command]
    public struct TryStartObstaclePassCommand
    {
        public Uid CharacterUid;
        public Uid ObstacleId;
    }
}