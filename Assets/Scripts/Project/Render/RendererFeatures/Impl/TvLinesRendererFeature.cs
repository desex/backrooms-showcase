using Project.Render.RenderPasses.Impl;
using Project.Render.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RendererFeatures.Impl
{
    public sealed class TvLinesRendererFeature : AOnePassRendererFeature<TvLinesRenderPass, TvDistortionSettings>
    {
        [SerializeField] private RenderPassEvent _renderOnEvent = RenderPassEvent.AfterRenderingPostProcessing;
        
        protected override string ShaderName => "Shader Graphs/TvLines";
        protected override RenderPassEvent RenderPassEvent => _renderOnEvent;
    }
}