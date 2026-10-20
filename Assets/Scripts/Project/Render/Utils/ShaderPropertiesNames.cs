using UnityEngine;

namespace Project.Render.Utils
{
    public static class ShaderPropertiesNames
    {
        public static readonly int UsePeriod = Shader.PropertyToID("_UsePeriod");
        public static readonly int ShaderStrength = Shader.PropertyToID("_ShaderStrength");
        public static readonly int Color = Shader.PropertyToID("_Color");
        public static readonly int Tiling = Shader.PropertyToID("_Tiling");
        public static readonly int Power = Shader.PropertyToID("_Power");
        
        //Square Glitching
        public static readonly int SquareGlitchingStrength = Shader.PropertyToID("_SquareGlitchingStrength");
        public static readonly int SquareGlitchingNoiseAmount = Shader.PropertyToID("_SquareGlitchingNoiseAmount");
        public static readonly int SquareGlitchingFlickeringStrength = Shader.PropertyToID("_SquareGlitchingFlickeringStrength");
        public static readonly int SquareGlitchingOnOffBorder = Shader.PropertyToID("_SquareGlitchingOnOffBorder");
        public static readonly int SquareGlitchingFrequencyModifier = Shader.PropertyToID("_SquareGlitchingFrequencyModifier");
        public static readonly int SquareGlitchingLengthModifier = Shader.PropertyToID("_SquareGlitchingLengthModifier");
        
        //Wave Glitching
        public static readonly int WaveGlitchingStrength = Shader.PropertyToID("_WaveGlitchingStrength");
        public static readonly int WaveGlitchingNoiseAmount = Shader.PropertyToID("_WaveGlitchingNoiseAmount");
        public static readonly int WaveGlitchingNoiseStrength = Shader.PropertyToID("_WaveGlitchingNoiseStrength");
        public static readonly int WaveGlitchingFlickeringStrength = Shader.PropertyToID("_WaveGlitchingFlickeringStrength");
        public static readonly int WaveGlitchingFrequencyModifier = Shader.PropertyToID("_WaveGlitchingFrequencyModifier");
        public static readonly int WaveGlitchingLengthModifier = Shader.PropertyToID("_WaveGlitchingLengthModifier");
        
        //Chromatic Aberrations
        public static readonly int AberrationCenter = Shader.PropertyToID("_AberrationCenter");
        public static readonly int AberrationsStrength = Shader.PropertyToID("_AberrationsStrength");
        public static readonly int AberrationOpacity = Shader.PropertyToID("_AberrationOpacity");
        public static readonly int AberrationFrequencyModifier = Shader.PropertyToID("_AberrationFrequencyModifier");
        public static readonly int AberrationLengthModifier = Shader.PropertyToID("_AberrationLengthModifier");

        //White Noise
        public static readonly int UseWhiteNoise = Shader.PropertyToID("_UseWhiteNoise");
        public static readonly int WhiteNoiseScale = Shader.PropertyToID("_WhiteNoiseScale");
        public static readonly int WhiteNoiseDissolve = Shader.PropertyToID("_WhiteNoiseDissolve");
        public static readonly int NoiseColor = Shader.PropertyToID("_NoiseColor");
        public static readonly int UseGradient = Shader.PropertyToID("_UseGradient");
        public static readonly int UseBlendScreen = Shader.PropertyToID("_UseBlendScreen");
        
        //Tv Lines
        public static readonly int LinesCount = Shader.PropertyToID("_LinesCount");
        public static readonly int LinesSpeed = Shader.PropertyToID("_LinesSpeed");
        public static readonly int LinesColor = Shader.PropertyToID("_LinesColor");
        public static readonly int LinesColorPower = Shader.PropertyToID("_LinesColorPower");
        public static readonly int LinesSizeBorders = Shader.PropertyToID("_LinesSizeBorders");
        public static readonly int DistortionSpeed = Shader.PropertyToID("_DistortionSpeed");
        public static readonly int DistortionStrength = Shader.PropertyToID("_DistortionStrength");
        public static readonly int DistortionNoiseScale = Shader.PropertyToID("_DistortionNoiseScale");
        
        //Speed
        public static readonly int Sides = Shader.PropertyToID("_Sides");
        public static readonly int Speed = Shader.PropertyToID("_Speed");
        public static readonly int MaskSize = Shader.PropertyToID("_Mask_Size");
        public static readonly int MaskContrast = Shader.PropertyToID("_Mask_Contrast");
        public static readonly int Smoothness = Shader.PropertyToID("_Smoothness");
    }
}