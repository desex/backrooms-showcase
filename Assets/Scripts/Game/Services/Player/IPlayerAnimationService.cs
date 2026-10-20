namespace Game.Services.Player
{
    public interface IPlayerAnimationService
    {
        void SetAllAnimationsSpeed(float speedRatio);
        void ResetAllAnimationsSpeed();
    }
}