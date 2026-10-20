using System;
using Animancer;
using Game.Utils.Animancer;
using UnityEngine;

namespace Game.Utils.Obstacles
{
    [Serializable]
    public class BreakBoxData
    {
        [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
        [field: SerializeField] public MotionClipTransition Animation { get; private set; }
        [field: SerializeField] public Transform PushTarget { get; private set; } 
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    }
}