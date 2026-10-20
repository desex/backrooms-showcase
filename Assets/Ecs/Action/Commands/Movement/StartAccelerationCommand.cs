using Ecs.Commands.Generator;
using Game.Utils.Movement;

namespace Ecs.Action.Commands.Movement
{
    [Command]
    public struct StartAccelerationCommand
    {
        public EObstacleTypeForAcceleration ObstacleTypeForAcceleration;
    }
}