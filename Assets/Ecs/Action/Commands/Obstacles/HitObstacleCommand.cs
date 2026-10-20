using Ecs.Commands.Generator;
using Game.Utils;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct HitObstacleCommand
    {
        public HitObstacleData Value;
    }
}