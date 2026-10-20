using Project.Render.Utils;

namespace Project.Render.RenderPasses.Impl
{
    public sealed class SpeedScriptableRenderPass : AOnePassScriptableRenderPass<SpeedEffectSettings>
    {
        protected override void UpdateSettings()
        {
            if (Material == null)
                return;
            
            Material.SetColor(ShaderPropertiesNames.Color, Settings.Color);
            Material.SetFloat(ShaderPropertiesNames.Sides, (int)Settings.EffectSide);
            Material.SetFloat(ShaderPropertiesNames.Speed, Settings.Speed);
            Material.SetFloat(ShaderPropertiesNames.MaskSize, Settings.MaskSize);
            Material.SetFloat(ShaderPropertiesNames.MaskContrast, Settings.MaskContrast);
            Material.SetFloat(ShaderPropertiesNames.Smoothness, Settings.Smoothness);
            Material.SetVector(ShaderPropertiesNames.Tiling, Settings.Tiling);
            Material.SetFloat(ShaderPropertiesNames.Power, Settings.Power);
        }
    }
}