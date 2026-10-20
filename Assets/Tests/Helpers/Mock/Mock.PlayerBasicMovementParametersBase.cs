using Db.Player;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class PlayerBasicMovementParametersBase : IPlayerBasicMovementParametersBase
        {
            private IPlayerBasicMovementParametersBase _playerBasicMovementParametersBaseImplementation;
            public float Speed { get; set; } = 10f;
            public float FastMoveRatio { get; set; } = 1.5f;
            public float UpSurfaceSpeed { get; set; } = 5f;
            public float MinimumSlopeDifference { get; } = 0.5f;
            public float InjuredSpeed { get; set; } = 6;
            public float ForwardSpeedAcceleration { get; set; } = 1f;
            public float ReboundHorizontalSpeed { get; set; }
            public float ReboundHorizontalSpeedOnRamp { get; }
            public float ChaserPresentationSlowDownHorizontalSpeed { get; } = 0.5f;
            public Vector3 GroundedSphereOffset { get; } = new Vector3(0, 0.2f, 0);
            public float GroundedSphereRadius { get; } = 0.5f;
            public float HorizontalSpeed { get; set; } = 10;
            public float InjuredHorizontalSpeed { get; set; } = 6;
            public float JumpHeight { get; set; } = 0.1f;
            public float GravityForce { get; set; } = -1f;
            public float ExtraGravityForce { get; set; } = -7f;
            public float WaterGravityForce { get; set; }
            public float CapMultiplayerWaterSpeed { get; set; }
            public float JumpHorizontalSpeedRatio { get; set; } = 1.2f;
            public float RotationSpeed { get; set; } = 2f;
        }
    }
}