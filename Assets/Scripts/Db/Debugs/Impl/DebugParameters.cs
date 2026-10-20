#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Debugs.Impl
{
    [CreateAssetMenu(menuName = "Settings/Debug/" + nameof(DebugParameters), fileName = nameof(DebugParameters))]
    public class DebugParameters : ScriptableObject, IDebugParameters
    {
        [field: SerializeField] 
        [field: SuffixLabel("seconds", Overlay = true)]
        public float FreezeDuration { get; private set; }
    }
}
#endif