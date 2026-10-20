using System;
using Project.Render.Utils.Enums;
using UnityEngine;

namespace Project.Render.Utils
{
    [Serializable]
    public class WhiteNoiseSettings
    {
        [Range(0f, 1f)] public float ShaderStrength = 1;
        
        [Space]
        [Header("White Noise")]
        public ENoiseType NoiseType = ENoiseType.Simple;
        public EBlendType BlendType = EBlendType.Multiply;
        public float WhiteNoiseScale = 2000;
        public float WhiteNoiseDissolve = 1;
        public Color Color = Color.gray;
    }
}