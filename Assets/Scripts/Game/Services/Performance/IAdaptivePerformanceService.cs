namespace Game.Services.Performance
{
    public interface IAdaptivePerformanceService
    {
        bool IsGpuTimeLowerThanNeeded { get; }
        bool TryDecreaseGraphicsSettings();
    }
}