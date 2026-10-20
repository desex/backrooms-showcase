using System;
using Ecs.Utils;
using UnityEngine;

namespace Project.Render.Utils
{
    [Serializable]
    public class SpeedEffectSettings
    {
        public float Speed = 1;
        
        [Range(0, 1)] public float MaskSize = 0.6f;
        
        [Range(0, 1)] public float MaskContrast = 0.8f;
        
        [Range(0, 10)] public float Smoothness = 5;
        
        [ColorUsage(showAlpha: true, hdr: true)] public Color Color;
        
        public Vector2 Tiling = new(3, 5);

        public ESpeedEffectAxis EffectSide;
        
        [Range(0, 1)] public float Power;
    }
}