using Db.Player;
using Ecs.Managers;

namespace Game.Services.Player.Impl
{
    public class PlayerSlideService : IPlayerSlideService
    {
        private readonly GameContext _game;
        private readonly IPlayerSlideAndJumpParameters _playerSlideParameters;

        public PlayerSlideService(GameContext game, IPlayerSlideAndJumpParameters playerSlideParameters)
        {
            _game = game;
            _playerSlideParameters = playerSlideParameters;
        }

        public int MaxSlidesCount { get; private set; } = 2;

        public int SwipeDownCount()
        {
            var player = _game.PlayerEntity;
            return player.SwipeDownCount.Value;
        }

        public void ChangeMaxSlideSCount(int maxSlideCount)
        {
            MaxSlidesCount = maxSlideCount;
        }

        public bool IsSlideAvailable(Uid uid)
        {
            var entity = _game.GetEntityWithUid(uid);
            var slideTime = entity.SlideDuration.Value;

            var secondSlideTime = _playerSlideParameters.SecondSlideTime;
            var thirdSlideTime = _playerSlideParameters.ThirdSlideTime;

            var swipeCount = SwipeDownCount();

            if (swipeCount > MaxSlidesCount || MaxSlidesCount == 1)
                return false;

            if (swipeCount <= MaxSlidesCount)
            {
                entity.ReplaceSlideDuration(slideTime + secondSlideTime);

                entity.IsUseExtraGravity = false;
            }
            else if (swipeCount == MaxSlidesCount)
            {
                entity.ReplaceSlideDuration(slideTime + thirdSlideTime);
            }

            return true;
        }
    }
}