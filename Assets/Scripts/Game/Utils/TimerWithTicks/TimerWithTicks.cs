using System;
using Ecs.Core.Interfaces;
using R3;

namespace Game.Utils.TimerWithTicks
{
    public class TimerWithTicks : IDisposable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly GameContext _game;
        private readonly ReactiveProperty<float> _normalizedLeftTime = new();
        private readonly ReactiveCommand _timerFinished = new();

        private IDisposable _timerDisposable;

        private float _needTime;
        private float _passedTime;

        public ReactiveProperty<float> NormalizedLeftTime => _normalizedLeftTime;
        public Observable<Unit> TimerFinished => _timerFinished;

        public TimerWithTicks(
            ITimeProvider timeProvider, 
            GameContext game
        )
        {
            _timeProvider = timeProvider;
            _game = game;
        }

        public void StartTimer(float time)
        {
            // Более показать не могу
        }

        public void StopTimer()
        {
            // Более показать не могу
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        private void HandleUpdate()
        {
            // Более показать не могу
        }

        private void HandleTimerFinish()
        {
            // Более показать не могу
        }
    }
}