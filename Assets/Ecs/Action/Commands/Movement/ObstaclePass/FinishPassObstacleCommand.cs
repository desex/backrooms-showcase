using Ecs.Commands.Generator;
using Ecs.Managers;

namespace Ecs.Action.Commands.Movement.ObstaclePass
{
    [Command]
    public struct FinishPassObstacleCommand
    {
        public Uid CharacterUid;
        public bool WasCompleted;
    }
}