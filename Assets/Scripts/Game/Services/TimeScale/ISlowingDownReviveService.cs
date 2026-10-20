namespace Game.Services.TimeScale
{
    public interface ISlowingDownReviveService
    {
        void CancelInterpolateTimeScale();
        void SmoothlyEnableTimeScaleAndGravity();
        void SmoothlyDisableTimeScaleAndGravity();
    }
}