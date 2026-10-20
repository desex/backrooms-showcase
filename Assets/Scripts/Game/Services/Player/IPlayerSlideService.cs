using Ecs.Managers;

namespace Game.Services.Player
{
    public interface IPlayerSlideService
    {
        int MaxSlidesCount { get; }

        int SwipeDownCount();

        bool IsSlideAvailable(Uid uid);

        void ChangeMaxSlideSCount(int maxSlideCount);
    }
}