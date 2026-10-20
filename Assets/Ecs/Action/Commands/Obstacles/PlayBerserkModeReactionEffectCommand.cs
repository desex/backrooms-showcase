using Ecs.Commands.Generator;
using Game.Utils.Obstacles;

namespace Ecs.Action.Commands.Obstacles
{
    [Command]
    public struct PlayBerserkModeReactionEffectCommand
    {
        public EObstacleEffectType Type;
    }
}