using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(OutOfBoundsKillPlayerParameters), fileName = nameof(OutOfBoundsKillPlayerParameters))]
    public class OutOfBoundsKillPlayerParameters : SerializedScriptableObject, IOutOfBoundsKillPlayerParameters
    {
        [field: Range(4f, 45f)]
        [field: SerializeField] public float KillDistance { get; private set; } = 20f;
    }
}