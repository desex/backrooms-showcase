using Project.Render.RenderPasses.Impl;
using Project.Render.Utils;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RendererFeatures.Impl
{
    public sealed class SpeedRendererFeature : AOnePassRendererFeature<SpeedScriptableRenderPass, SpeedEffectSettings>
    {
        protected override string ShaderName => "Shader Graphs/SpeedEffect";
        protected override RenderPassEvent RenderPassEvent => RenderPassEvent.AfterRenderingPostProcessing;
    }
}