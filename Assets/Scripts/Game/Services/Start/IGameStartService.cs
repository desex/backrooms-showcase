
using R3;

namespace Game.Services.Start
{
    public interface IGameStartService
    {
        int DaysSinceReg { get; }
        int SessionNumber { get; }
        int ApplicationLaunchCount { get; }
        long FirstLaunchTimestamp { get; }

        ReadOnlyReactiveProperty<bool> NeedSkipMainMenu { get; }
        ReadOnlyReactiveProperty<bool> IsTransitingToAnotherLevel { get; }

        void SetSkipMainMenu(bool needSkipMainMenu);
        void SetTransitionFlag(bool isTransitioning);
    }
}