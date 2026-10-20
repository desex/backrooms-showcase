using DG.Tweening;

namespace Db.TimeScale
{
    public interface ISlowingDownReviveParameters
    {
        float TimeScaleInterpolateEnable { get; }
        float TimeScaleInterpolateDisable{ get; }
        float ForceGravityModifier { get; }
        float TimeReachGravityTarget { get; }
        Ease SlowdownEase { get; }
        Ease DisableSlowdownEase { get; }
    }
}