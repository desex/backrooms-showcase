using System;
using Animancer;
using Game.Utils.Movement;
using UnityEngine;

namespace Game.Utils.Animancer
{
    [Serializable]
    [EventNames(typeof(EAnimancerEvents))]
    public class MotionLinearMixerTransition : LinearMixerTransition
    {
        [SerializeField] private bool applyRootMotion;
        
        public EAnimationLayer AnimationLayer;

        /// <inheritdoc />
        public override void Apply(AnimancerState state)
        {
            // Более показать не могу
        }
    }
}