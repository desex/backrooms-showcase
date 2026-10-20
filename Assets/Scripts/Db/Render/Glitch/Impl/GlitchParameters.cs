using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Render.Glitch.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(GlitchParameters), fileName = nameof(GlitchParameters))]
    public class GlitchParameters : SerializedScriptableObject, IGlitchParameters
    {
        [field: SerializeField] public GlitchData ChaserData { get; private set; }
        [field: SerializeField] public GlitchData KillData { get; private set; }
    }
}