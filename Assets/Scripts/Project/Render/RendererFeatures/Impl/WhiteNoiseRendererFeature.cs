using Project.Render.RenderPasses.Impl;
using Project.Render.Utils;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RendererFeatures.Impl
{
    public class WhiteNoiseRendererFeature : AOnePassRendererFeature<WhiteNoiseRenderPass, WhiteNoiseSettings>
    {
        protected override string ShaderName => "ScreenSpace/WhiteNoise";
        protected override RenderPassEvent RenderPassEvent => RenderPassEvent.AfterRenderingPostProcessing;
    }
}