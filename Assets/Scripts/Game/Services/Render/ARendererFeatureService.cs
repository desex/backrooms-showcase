using System;
using Game.Providers;
using Game.Utils.Render;
using UnityEngine.Rendering.Universal;

namespace Game.Services.Render
{
    public abstract class ARendererFeatureService<T> where T : ScriptableRendererFeature
    {
        private readonly T _defaultPipelineRendererFeature;
        private readonly T _uiCameraPipelineRendererFeature;

        protected ARendererFeatureService(IRendererFeatureProvider rendererFeatureProvider, bool useDefaultAsset, bool useCameraAsset)
        {
            if (useDefaultAsset)
                _defaultPipelineRendererFeature = rendererFeatureProvider.FindRendererFeature<T>(EPipelineAssetType.Default);
            
            if (useCameraAsset)
                _uiCameraPipelineRendererFeature = rendererFeatureProvider.FindRendererFeature<T>(EPipelineAssetType.UiCamera);
        }
        
        protected T GetRendererFeature(
            EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default
        )
        {
            return pipelineAssetType switch
            {
                EPipelineAssetType.Default => _defaultPipelineRendererFeature,
                EPipelineAssetType.UiCamera => _uiCameraPipelineRendererFeature,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}