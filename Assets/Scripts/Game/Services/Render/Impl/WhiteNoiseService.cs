using Game.Providers;
using Game.Utils.Render;
using Project.Render.RendererFeatures.Impl;

namespace Game.Services.Render.Impl
{
    public sealed class WhiteNoiseService : ARendererFeatureService<WhiteNoiseRendererFeature>, IWhiteNoiseService
    {
        public WhiteNoiseService(
            IRendererFeatureProvider rendererFeatureProvider
        ) : base(rendererFeatureProvider, true, true) { }
        
        public void StartWhiteNoiseImmediately(EPipelineAssetType pipelineAssetType)
        {
            var feature = GetRendererFeature(pipelineAssetType);
            feature.Settings.ShaderStrength = 1;
            feature.SetActive(true);
        }

        public void StopWhiteNoiseImmediately(EPipelineAssetType pipelineAssetType)
        {
            var feature = GetRendererFeature(pipelineAssetType);
            feature.Settings.ShaderStrength = 0;
            feature.SetActive(false);
        }
    }
}