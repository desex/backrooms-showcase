using System;
using Db.Graphics;
using Game.Utils.Render;
using UnityEngine.Rendering.Universal;

namespace Game.Providers.Impls
{
    public class RendererFeatureProvider : IRendererFeatureProvider
    {
        private readonly IUrpSettingsBase _urpSettingsBase;

        public RendererFeatureProvider(IUrpSettingsBase urpSettingsBase)
        {
            _urpSettingsBase = urpSettingsBase;
        }

        public ScriptableRendererFeature FindRendererFeature(string featureName, EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default)
        {
            // Более показать не могу
            return default;
        }
        
        public T FindRendererFeature<T>(EPipelineAssetType pipelineAssetType = EPipelineAssetType.Default) where T : ScriptableRendererFeature
        {
            // Более показать не могу
            return default;
        }
    }
}