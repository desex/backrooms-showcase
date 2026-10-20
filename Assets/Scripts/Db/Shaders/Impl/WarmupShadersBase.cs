using UnityEngine;

namespace Db.Shaders.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(WarmupShadersBase), fileName = nameof(WarmupShadersBase))]
    public sealed class WarmupShadersBase : ScriptableObject, IWarmupShadersBase
    {
        [field: SerializeField] public Shader[] WarmupShaders { get; private set; }
    }
}