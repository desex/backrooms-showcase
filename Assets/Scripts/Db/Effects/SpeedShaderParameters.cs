using System;
using UnityEngine;

namespace Db.Effects
{
    [Serializable]
    public struct SpeedShaderParameters
    {
        [Range(0, 1)]
        public float MaskSize;
        
        [Range(0, 1)]
        public float MaskContrast;

        [Range(0, 10)]
        public float Smoothness;

        [Range(0, 1)]
        public float Power;
    }
}