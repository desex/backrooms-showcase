using Project.Render.RenderPasses.Impl;
using Project.Render.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project.Render.RendererFeatures.Impl
{
    public sealed class GlitchRendererFeature : AOnePassRendererFeature<GlitchScriptableRenderPass, GlitchSettings>
    {
        [SerializeField] private RenderPassEvent _renderOnEvent = RenderPassEvent.AfterRenderingPostProcessing;
        [SerializeField] private RenderTextureFormat _renderTextureFormat = RenderTextureFormat.ARGB4444;
        
        protected override string ShaderName => "ScreenSpace/Glitch";
        protected override RenderPassEvent RenderPassEvent => _renderOnEvent;
        protected override RenderTextureFormat RenderTextureFormat => _renderTextureFormat;
    }
}