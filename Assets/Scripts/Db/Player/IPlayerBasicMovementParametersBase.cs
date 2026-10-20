using UnityEngine;

namespace Db.Player
{
    public interface IPlayerBasicMovementParametersBase
    {
        float Speed { get; }
        float FastMoveRatio { get; }
        float UpSurfaceSpeed { get; }
        float MinimumSlopeDifference { get; }
        float InjuredSpeed { get; }
        float ForwardSpeedAcceleration { get; }
        float GravityForce { get; }
        float ExtraGravityForce { get; }
        
        float JumpHorizontalSpeedRatio { get; }
        float RotationSpeed { get; }
        float HorizontalSpeed { get; }
        float InjuredHorizontalSpeed { get; }
        float ReboundHorizontalSpeed { get; }
        float ReboundHorizontalSpeedOnRamp { get; }
        
        float ChaserPresentationSlowDownHorizontalSpeed { get; }
        
        Vector3 GroundedSphereOffset { get; }
        float GroundedSphereRadius { get; }
    }
}