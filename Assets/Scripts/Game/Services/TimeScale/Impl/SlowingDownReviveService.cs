using System;
using Db.TimeScale;
using DG.Tweening;
using Game.Services.GameState;

namespace Game.Services.TimeScale.Impl
{
    public class SlowingDownReviveService : ISlowingDownReviveService
    {
        private const float TIME_SCALE_NORMAL = 1;
        private const float TIME_SCALE_PAUSE = 0;
        private const float ZERO_GRAVITY = 0;
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly ISlowingDownReviveParameters _slowingDownReviveParameters;
        private readonly ITimeScaleService _timeScaleService;

        private float _cachedVerticalVelocity;

        private Sequence _timeScaleSequence;
        private Sequence _velocitySequence;

        public SlowingDownReviveService(
            ISlowingDownReviveParameters slowingDownReviveParameters,
            ITimeScaleService timeScaleService,
            IGameStateService gameStateService,
            GameContext game
        )
        {
            _game = game;
            _slowingDownReviveParameters = slowingDownReviveParameters;
            _timeScaleService = timeScaleService;
            _gameStateService = gameStateService;
        }

        public void CancelInterpolateTimeScale()
        {
            // Более показать не могу
        }

        public void SmoothlyEnableTimeScaleAndGravity()
        {
            // Более показать не могу
        }

        public void SmoothlyDisableTimeScaleAndGravity()
        {
            // Более показать не могу
        }

        private void ApplyGravityAdjustment(float targetTimeScale, float duration, Ease ease)
        {
            // Более показать не могу
        }

        private void ApplyTimeScaleAndGravitySmoothly(
            float endValueGravity,
            Ease ease,
            float duration,
            float targetTimeScale,
            Action onCompleteAction
        )
        {
            // Более показать не могу
        }

        private Tween CreateGravityTween(float startGravity, float endValueGravity, float duration, Ease ease)
        {
            // Более показать не могу
            return default;
        }
    }
}