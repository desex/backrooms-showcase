using DG.Tweening;

namespace Db.Obstacles
{
    public interface IBreakBoxParameters
    {
        float BaseForce { get; }
        float ForceMultiplier { get; }
        float NormalSpeedForceBonus { get; }
        float VerticalBoost { get; }
        float TimerBoxPhysicsOff { get; }
        
        float AnimationSpeedMin { get; }
        float AnimationSpeedMax { get; }
        
        float PlayerSpeedMin { get; }
        float PlayerSpeedMax { get; }
        float PlayerDashSpeedMax { get; }
        float DashForceMin { get; }
        float DashForceMax { get; }
        float EndVelocityMultiplier { get; }
        Ease EasingEndDash { get; }

        float DistanceFlightThresholdMin { get; }
        float DistanceFlightThresholdMax { get; }
        float SlowingMultiplier { get; }
        
        float AnimationBoxOffsetMin { get; }
        float AnimationBoxOffsetMax { get; }
        float AnimationBoxOffsetMinInDash { get; }
        float AnimationBoxOffsetMaxInDash { get; }
        float AnimationBoxMoveTimer { get; }
        Ease EasingAnimationBox { get; }
    }
}