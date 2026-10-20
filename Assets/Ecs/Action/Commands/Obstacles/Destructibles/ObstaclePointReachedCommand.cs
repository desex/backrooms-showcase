using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Obstacles.Destructibles
{
    [Command]
    public struct ObstaclePointReachedCommand
    {
        public Uid ObstacleUid;
        public Uid CharacterUid;
        public int PointIndex;
    }
}