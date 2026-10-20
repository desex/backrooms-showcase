using System;
using Ecs.Utils;
using Game.Utils.Analytics;
using JCMG.EntitasRedux;
using R3;
using Zenject;

namespace Game.Services.Analytics.Impl
{
    public class LevelSessionService : ILevelSessionService, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly GameContext _game;
        private DateTime _levelFinishTime;

        private EAnalyticsSessionState _sessionState = EAnalyticsSessionState.Start;

        private DateTime _startLevelTime;

        private DateTime? _startPauseTime;
        private TimeSpan _totalPauseTime;

        public LevelSessionService(GameContext game)
        {
            _game = game;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public int TimeSpent
        {
            get
            {
                if (_sessionState == EAnalyticsSessionState.Start)
                    return 0;

                var isLevelFinished = _sessionState == EAnalyticsSessionState.Finished;

                var endTime = isLevelFinished ? _levelFinishTime : DateTime.Now;

                var time = endTime - _startLevelTime;

                var currentTotalPauseTime = _totalPauseTime;

                if (!isLevelFinished && _startPauseTime.HasValue)
                {
                    var timeInCurrentPause = DateTime.Now - _startPauseTime.Value;
                    currentTotalPauseTime += timeInCurrentPause;
                }

                var seconds = time.TotalSeconds - currentTotalPauseTime.TotalSeconds;
                return (int)seconds;
            }
        }

        private void HandleStartLevel()
        {
            // Более показать не могу
        }

        private void HandleLevelFinish()
        {
            // Более показать не могу
        }

        private void OnEntityCreated(IContext context, IEntity entity)
        {
            // Более показать не могу
        }

        private void OnGameState(GameEntity entity, EGameState value)
        {
            // Более показать не могу
        }

        private void OnPause(GameEntity entity, bool isPaused)
        {
            // Более показать не могу
        }

        private void TryAccumulatePausedTime()
        {
            // Более показать не могу
        }
    }
}