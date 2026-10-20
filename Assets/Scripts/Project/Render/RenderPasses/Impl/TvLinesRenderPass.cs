using Project.Render.Utils;

namespace Project.Render.RenderPasses.Impl
{
    public sealed class TvLinesRenderPass : AOnePassScriptableRenderPass<TvDistortionSettings>
    {
        protected override void UpdateSettings()
        {
            if (Material == null)
                return;
            
            Material.SetFloat(ShaderPropertiesNames.LinesCount, Settings.LinesCount);
            Material.SetFloat(ShaderPropertiesNames.LinesSpeed, Settings.LinesSpeed);
            Material.SetColor(ShaderPropertiesNames.LinesColor, Settings.LinesColor);
            Material.SetFloat(ShaderPropertiesNames.LinesColorPower, Settings.LinesColorPower);
            Material.SetVector(ShaderPropertiesNames.LinesSizeBorders, Settings.LinesSizeBorders);
            Material.SetFloat(ShaderPropertiesNames.DistortionSpeed, Settings.DistortionSpeed);
            Material.SetFloat(ShaderPropertiesNames.DistortionStrength, Settings.DistortionStrength);
            Material.SetFloat(ShaderPropertiesNames.DistortionNoiseScale, Settings.DistortionNoiseScale);
            Material.SetFloat(ShaderPropertiesNames.ShaderStrength, Settings.ShaderStrength);
        }
    }
}