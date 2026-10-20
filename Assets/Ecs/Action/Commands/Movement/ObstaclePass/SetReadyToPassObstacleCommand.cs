using Ecs.Commands.Generator;
using Ecs.Managers;
using Game.Utils.Movement;

namespace Ecs.Action.Commands.Movement.ObstaclePass
{
    [Command]
    public struct SetReadyToPassObstacleCommand
    {
        public Uid CharacterUid;
        public ObstaclePassData ObstaclePassData;
    }
}