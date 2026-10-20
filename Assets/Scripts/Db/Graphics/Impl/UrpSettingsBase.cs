using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Db.Graphics.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(UrpSettingsBase), fileName = nameof(UrpSettingsBase))]
    public class UrpSettingsBase : ScriptableObject, IUrpSettingsBase
    {
        [field: SerializeField] public UniversalRenderPipelineAsset UniversalRenderPipelineAsset { get; private set; }
        [field: SerializeField] public UniversalRendererData DefaultRendererData { get; private set; }
        [field: SerializeField] public UniversalRendererData UiCameraRendererData { get; private set; }
    }
}