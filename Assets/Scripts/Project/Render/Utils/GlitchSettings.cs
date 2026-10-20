using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Render.Utils
{
    [Serializable]
    public sealed class GlitchSettings
    {
        public bool UsePeriod = true;
        [Range(0f, 1f)] public float ShaderStrength = 1;
        
        [Space]
        [Header("Square Glitching")]
        [Range(0f, 1f)] 
        public float SquareGlitchingStrength = 1f;
        public float SquareGlitchingNoiseAmount = 2f;
        public float SquareGlitchingFlickeringStrength = 0.3f;
        [Range(-1f, 1f)] public float SquareGlitchingOnOffBorder = -0.2f;
        public PeriodSettings SquareGlitchingPeriodSettings = new PeriodSettings()
        {
            FrequencyModifier = 3,
            LengthModifier = 0.4f,
        };
        
        [Space]
        [Header("Wave Glitching")]
        [Range(0f, 1f)] 
        public float WaveGlitchingStrength = 1f;
        public float WaveGlitchingNoiseAmount = 17f;
        [Range(0f, 1f)] public float WaveGlitchingNoiseStrength = 0.01f;
        public float WaveGlitchingFlickeringStrength = 0.3f;
        public PeriodSettings WaveGlitchingPeriodSettings = new PeriodSettings()
        {
            FrequencyModifier = 3,
            LengthModifier = 0.4f,
        };

        [Space]
        [Header("Chromatic Aberrations")]
        [Range(0f, 0.01f)] 
        public float AberrationsStrength = 0.007f;
        public Vector2 AberrationCenter = new Vector2(0.5f, 0.5f);
        [Range(0f, 1f)] public float AberrationOpacity = 0.8f;
        public PeriodSettings AberrationPeriodSettings = new PeriodSettings()
        {
            FrequencyModifier = 3,
            LengthModifier = 0.25f,
        };

        [Space]
        [Header("White Noise")]
        public bool UseWhiteNoise = true;
        public float WhiteNoiseScale = 2000;
        public float WhiteNoiseDissolve = 20;

        [Serializable]
        public struct PeriodSettings
        {
            [MinValue(0f)] public float FrequencyModifier;
            [Range(0f, 2f)] public float LengthModifier;
        }
    }
}