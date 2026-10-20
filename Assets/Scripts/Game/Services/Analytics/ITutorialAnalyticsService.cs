using Game.Utils.Analytics;

namespace Game.Services.Analytics
{
    public interface ITutorialAnalyticsService
    {
        void HandleTutorialStateStart(EAnalyticsTutorialState tutorialState);
        void HandleTutorialStateEnd(EAnalyticsTutorialState tutorialState);
        void HandleTutorialStateRestart();
        void CancelTutorialStateAfterRevive();
        void HandleTutorialsFinish();
    }
}