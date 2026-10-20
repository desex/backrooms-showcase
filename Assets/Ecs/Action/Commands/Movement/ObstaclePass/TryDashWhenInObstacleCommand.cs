using Ecs.Commands.Generator;
using Ecs.Managers;
using Ecs.Utils.ObstaclePass;

namespace Ecs.Action.Commands.Movement.ObstaclePass
{
    [Command]
    public struct TryDashWhenInObstacleCommand
    {
        public Uid CharacterUid;
        public EPassObstacleType PassObstacleType;
    }
}