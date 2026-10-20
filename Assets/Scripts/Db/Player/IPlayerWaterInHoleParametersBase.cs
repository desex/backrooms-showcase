using DG.Tweening;

namespace Db.Player
{
    public interface IPlayerWaterInHoleParametersBase
    {
        float InWaterForwardSpeed { get; }
        
        float InWaterAnimationSpeedMultiplier { get; }
        float InWaterForwardSpeedDeceleration { get; }
        
        float FastWaterEnterVerticalSpeed { get; }
        float FastWaterEnterDuration { get; }
        Ease FastWaterEnterEase { get; }
        
        float StayInWaterVerticalSpeed { get; }
        float SlowdownAfterEnterDuration { get; }
        Ease SlowdownAfterEnterEase { get; }
    }
}