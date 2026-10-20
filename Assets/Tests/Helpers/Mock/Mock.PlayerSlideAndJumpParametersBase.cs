using Db.Player;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class PlayerSlideAndJumpParameters : IPlayerSlideAndJumpParameters
        {
            public float SwipeThreshold { get; set; } = 0.3f;
            public float SlideRollAnimationThreshold { get; } = 0.1f;
            public float SlideHeight { get; set; } = 0.5f;
            public Vector3 SlideChangeCenter { get; set; } = new Vector3(0.2f, 0.3f, 0);
            public float SlideTime { get; set; } = 0.6f;
            public float TargetSlideSpeed { get; set; } = 80;
            public float SecondSlideTime { get; set; } = 0.48f;
            public float ThirdSlideTime { get; set; } = 0.3f;
            public float SlideRadius { get; set; } = 0.25f;
            public float ControllableSlideSpeed { get; set; } = 100f;
            public float ControllableSlideHorizontalSpeed { get; set; } = 0.1f;
            public float ControllableSlideAcceleration { get; set; } = 4f;
            public float TimeToEndControllableSliding { get; set; } = 0.2f;
            public float JumpHeight { get; set; } = 0.06f;
            public float AfterJumpDelay { get; set; } = 0.4f;
            public float JumpForce { get; set; } = 0.6f;
            public float DoubleJumpForce { get; set; } = 0.4f;
            public float DoubleJumpHeight { get; set; } = 0.03f;
            public float DoubleJumpInHoleHeight { get; set; } = 0.07f;
            public float DoubleJumpSpeed { get; set; } = 40f;
            public float RollDuration { get; set; } = 1f;
            public float LandDuration { get; } = 0.37f;
            public float ControllableSlideCharacterControllerRadiusSize { get; } = 0.49f;
            public float DefaultCharacterControllerRadiusSize { get; } = 0.25f;
            public float RollRadius { get; set; } = 0.28f;
            public float DisableDashAfterDoubleJumpDuration { get; } = 0.1f;
            public float ControllableSlideSpeedDecreaseMultiplier { get; } = .5f;
        }
    }
}