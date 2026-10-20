using Animancer;
using Ecs.Utils;
using Game.Utils.Animancer;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public class TravelatorAnimationModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _idleAnimationClip;
        
        public override void Activate()
        {
            // Более показать не могу
        }
        
        public override void Deactivate()
        {
            // Более показать не могу
        }
    }
}