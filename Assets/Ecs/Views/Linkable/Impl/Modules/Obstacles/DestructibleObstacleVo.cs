using System;
using Animancer;
using Ecs.Utils.ObstaclePass;
using Game.Utils.Animancer;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    [Serializable]
    public class DestructibleObstacleVo
    {
        public EPassObstacleType PassObstacleType;
        public AnimancerComponent AnimancerComponent;
        public MotionClipTransition MotionClipTransition;
    }
}