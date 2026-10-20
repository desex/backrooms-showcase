using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerAccelerationParametersBase), fileName = nameof(PlayerAccelerationParametersBase))]
    public class PlayerAccelerationParametersBase : ScriptableObject, IPlayerAccelerationParametersBase 
    {
        [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; }
        [field: SerializeField] public float AccelerationFactor { get; private set; }
        [field: SerializeField] public float RunAnimationSpeedIncrease { get; private set; }
        [field: SerializeField] public float SpeedBoostAndRunAnimationFadeDuration { get; private set; }
        [field: SerializeField] public float AccelerationDuration { get; private set; }
    }
}