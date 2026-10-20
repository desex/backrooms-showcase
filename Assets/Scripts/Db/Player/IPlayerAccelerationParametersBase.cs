using UnityEngine;

namespace Db.Player
{
    public interface IPlayerAccelerationParametersBase
    {
        float RunAnimationSpeedIncrease { get; }
        float SpeedBoostAndRunAnimationFadeDuration { get; }
        float AccelerationFactor { get; }
        float AccelerationDuration { get; }
        AnimationCurve AccelerationCurve { get; }
    }
}