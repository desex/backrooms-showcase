using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerPlankRunParametersBase), fileName = nameof(PlayerPlankRunParametersBase))]
    public class PlayerPlankRunParametersBase : ScriptableObject, IPlayerPlankRunParametersBase
    {
        [field: SerializeField] public float CentralizeDuration { get; private set; }
        [field: Range(0.5f, 2f)]
        [field: SerializeField] public float SlowDownRatioIncreaseSpeed { get; private set; } = 1.3f;
        [field: Range(0.5f, 2f)]
        [field: SerializeField] public float SlowDownRatioDecreaseSpeed { get; private set; } = 1.3f;
        [field: Range(0, 1f)]
        [field: SerializeField] public float MaxSlowDownRatio { get; set; } = 0.2f;
    }
}