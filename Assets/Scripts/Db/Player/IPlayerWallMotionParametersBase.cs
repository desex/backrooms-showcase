using UnityEngine;

namespace Db.Player
{
    public interface IPlayerWallMotionParametersBase
    {
        float Speed { get; }
        float RunAcceleration { get; }
        float DurationPerUnit { get; }
        AnimationCurve VelocityCurve { get; }
        float VelocityModifier { get; }
        float JumpForce { get; }
        float JumpHeight { get; }
        float DelayAfterJump { get; }
    }
}