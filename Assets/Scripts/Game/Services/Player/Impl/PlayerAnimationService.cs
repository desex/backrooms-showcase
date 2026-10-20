using Animancer;
using Game.Utils.Movement;

namespace Game.Services.Player.Impl
{
    public class PlayerAnimationService : IPlayerAnimationService
    {
        private const float DEFAULT_ANIMATION_SPEED = 1f;

        private readonly GameContext _game;

        public PlayerAnimationService(GameContext game)
        {
            _game = game;
        }

        public void SetAllAnimationsSpeed(float speedRatio)
        {
            var defaultLayer = GetDefaultLayer();
            defaultLayer.Speed = speedRatio;
        }

        public void ResetAllAnimationsSpeed()
        {
            var defaultLayer = GetDefaultLayer();
            defaultLayer.Speed = DEFAULT_ANIMATION_SPEED;
        }

        private AnimancerLayer GetDefaultLayer()
        {
            var animancer = _game.PlayerEntity.Animancer.Value;
            var defaultLayer = animancer.Layers[(int)EAnimationLayer.Default];
            return defaultLayer;
        }
    }
}