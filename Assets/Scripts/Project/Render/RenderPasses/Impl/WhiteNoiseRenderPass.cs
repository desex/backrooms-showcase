using Project.Render.Utils;
using UnityEngine;

namespace Project.Render.RenderPasses.Impl
{
    public class WhiteNoiseRenderPass : AOnePassScriptableRenderPass<WhiteNoiseSettings>
    {
        protected override void UpdateSettings()
        {
            if (Material == null)
                return;
            
            Material.SetInteger(ShaderPropertiesNames.UseGradient, (int)Settings.NoiseType - 1);
            Material.SetInteger(ShaderPropertiesNames.UseBlendScreen, (int)Settings.BlendType - 1);
            Material.SetFloat(ShaderPropertiesNames.ShaderStrength, Settings.ShaderStrength);
            Material.SetFloat(ShaderPropertiesNames.WhiteNoiseScale, Settings.WhiteNoiseScale);
            Material.SetFloat(ShaderPropertiesNames.WhiteNoiseDissolve, Settings.WhiteNoiseDissolve);
            Material.SetVector(ShaderPropertiesNames.NoiseColor, new Vector4(Settings.Color.r, Settings.Color.g, Settings.Color.b, 0f));
        }
    }
}