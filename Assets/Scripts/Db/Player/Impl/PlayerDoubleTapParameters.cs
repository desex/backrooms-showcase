using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerDoubleTapParameters), fileName = nameof(PlayerDoubleTapParameters))]
    public class PlayerDoubleTapParameters : ScriptableObject, IPlayerDoubleTapParameters
    {
        [field: SerializeField] public float DoubleTapThreshold { get; private set; }
        [field: SerializeField] public float DoubleTapThresholdCooldown { get; private set; }
    }
}