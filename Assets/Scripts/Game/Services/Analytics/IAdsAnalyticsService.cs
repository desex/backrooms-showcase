using Plugins.SimpleAdWrapper;

namespace Game.Services.Analytics
{
    public interface IAdsAnalyticsService
    {
        void HandleAdTriggered(EAdType adType, string placement);
        void HandleAdStarted(EAdType adType, string placement);
        void HandleAdCompleted(ShowAdResult showAdResult, string placement);
    }
}