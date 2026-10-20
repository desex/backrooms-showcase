using Project.Render.Utils;

namespace Project.Render.RenderPasses.Impl
{
    public sealed class GlitchScriptableRenderPass : AOnePassScriptableRenderPass<GlitchSettings>
    {
        protected override void UpdateSettings()
        {
            if (Material == null)
                return;
            
            Material.SetInteger(ShaderPropertiesNames.UsePeriod, Settings.UsePeriod ? 1 : 0);
            Material.SetFloat(ShaderPropertiesNames.ShaderStrength, Settings.ShaderStrength);
            
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingStrength, Settings.SquareGlitchingStrength);
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingNoiseAmount, Settings.SquareGlitchingNoiseAmount);
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingFlickeringStrength, Settings.SquareGlitchingFlickeringStrength);
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingOnOffBorder, Settings.SquareGlitchingOnOffBorder);
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingFrequencyModifier, Settings.SquareGlitchingPeriodSettings.FrequencyModifier);
            Material.SetFloat(ShaderPropertiesNames.SquareGlitchingLengthModifier, Settings.SquareGlitchingPeriodSettings.LengthModifier);
            
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingStrength, Settings.WaveGlitchingStrength);
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingNoiseAmount, Settings.WaveGlitchingNoiseAmount);
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingNoiseStrength, Settings.WaveGlitchingNoiseStrength);
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingFlickeringStrength, Settings.WaveGlitchingFlickeringStrength);
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingFrequencyModifier, Settings.WaveGlitchingPeriodSettings.FrequencyModifier);
            Material.SetFloat(ShaderPropertiesNames.WaveGlitchingLengthModifier, Settings.WaveGlitchingPeriodSettings.LengthModifier);
            
            Material.SetVector(ShaderPropertiesNames.AberrationCenter, Settings.AberrationCenter);
            Material.SetFloat(ShaderPropertiesNames.AberrationsStrength, Settings.AberrationsStrength);
            Material.SetFloat(ShaderPropertiesNames.AberrationOpacity, Settings.AberrationOpacity);
            Material.SetFloat(ShaderPropertiesNames.AberrationFrequencyModifier, Settings.AberrationPeriodSettings.FrequencyModifier);
            Material.SetFloat(ShaderPropertiesNames.AberrationLengthModifier, Settings.AberrationPeriodSettings.LengthModifier);
            
            Material.SetInteger(ShaderPropertiesNames.UseWhiteNoise, Settings.UseWhiteNoise ? 1 : 0);
            Material.SetFloat(ShaderPropertiesNames.WhiteNoiseScale, Settings.WhiteNoiseScale);
            Material.SetFloat(ShaderPropertiesNames.WhiteNoiseDissolve, Settings.WhiteNoiseDissolve);
        }
    }
}