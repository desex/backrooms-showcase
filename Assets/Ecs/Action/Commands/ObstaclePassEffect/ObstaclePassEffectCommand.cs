using Ecs.Commands.Generator;
using Ecs.Utils;

namespace Ecs.Action.Commands.ObstaclePassEffect
{
    [Command]
    public struct ObstaclePassEffectCommand
    {
        public ESpeedEffectAxis SpeedEffectAxis;
    }
}