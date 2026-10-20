using UnityEngine.Rendering.Universal;

namespace Db.Graphics
{
    public interface IUrpSettingsBase
    {
        UniversalRenderPipelineAsset UniversalRenderPipelineAsset { get; }
        UniversalRendererData DefaultRendererData { get; }
        UniversalRendererData UiCameraRendererData { get; }
    }
}