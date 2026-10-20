namespace Db.Player
{
    public interface IPlayerSlideAndJumpParameters
    {
        float SwipeThreshold { get; }
        
        float SlideRollAnimationThreshold { get; }
        float SlideTime { get; }
        float TargetSlideSpeed { get; }
        float SecondSlideTime { get; }
        float ThirdSlideTime { get; }
        float ControllableSlideSpeed { get; }
        float ControllableSlideHorizontalSpeed { get; }
        float ControllableSlideAcceleration { get; }
        float TimeToEndControllableSliding { get; }
        float ControllableSlideSpeedDecreaseMultiplier { get; }
        
        float JumpHeight { get; }
        float JumpForce { get; }
        float DoubleJumpForce { get; }
        float DoubleJumpHeight { get; }
        float DoubleJumpInHoleHeight { get; }
        float DoubleJumpSpeed { get; }
        
        float RollDuration { get; }

        float ControllableSlideCharacterControllerRadiusSize { get; }
        float DefaultCharacterControllerRadiusSize { get; }
        
        float LandDuration { get; }
    }
}