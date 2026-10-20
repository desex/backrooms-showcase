using Game.Utils.Render;
using UnityEngine.Rendering.Universal;

namespace Game.Providers
{
    public interface IRendererFeatureProvider
    {
        ScriptableRendererFeature FindRendererFeature(string featureName, EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default);
        T FindRendererFeature<T>(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default) where T : ScriptableRendererFeature;
    }
}