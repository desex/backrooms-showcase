using System;
using PdUtils.PlayerPrefs;
using UnityEngine;
using Zenject;

namespace Core
{
    public interface ISessionService
    {
        int TotalPlayTimeMin { get; }
        int TotalPlayTimeSec { get; }
        int DaysInGame { get; }
        int RestartCount { get; }
        int TotalRestartCount { get; }
        int LevelTransitionsInRunCount { get; }
        int TotalLevelTransitionsCount { get; }
        
        void IncreaseRestartCount();
        void IncreaseTransitionsCount();
    }

    public class SessionService : ISessionService, IInitializable, IDisposable, ITickable
    {
        private const string TOTAL_RESTART_COUNT_KEY = "TotalRestartCount";
        private const string TOTAL_LEVEL_TRANSITIONS_COUNT_KEY = "TotalLevelTransitionsCount";
        private const string TOTAL_SESSIONS_TIME_KEY = "TotalSessionTime";
        private const string SESSION_STAMP_KEY = "SessionStamp";
        private const string DAYS_IN_GAME_KEY = "DaysInGame";
        private const string NEXT_DAY_KEY = "NextDay";

        private const int AUTO_SESSION_REFRESH_S = 60;

        private readonly IPlayerPrefsManager _playerPrefsManager;

        private float _accumulator;

        public SessionService(
            IPlayerPrefsManager playerPrefsManager
        )
        {
            _playerPrefsManager = playerPrefsManager;
        }

        public int RestartCount { get; private set; }
        public int LevelTransitionsInRunCount { get; private set; }
        
        public int TotalRestartCount
        {
            get
            {
                if (!_playerPrefsManager.HasKey(TOTAL_RESTART_COUNT_KEY))
                    return 0;
                
                return _playerPrefsManager.GetValue<int>(TOTAL_RESTART_COUNT_KEY);
            }
        }

        public int TotalLevelTransitionsCount
        {
            get
            {
                if (!_playerPrefsManager.HasKey(TOTAL_LEVEL_TRANSITIONS_COUNT_KEY))
                    return 0;

                return _playerPrefsManager.GetValue<int>(TOTAL_LEVEL_TRANSITIONS_COUNT_KEY);
            }
        }

        public int TotalPlayTimeMin
        {
            get
            {
                UpdateSessionTime();

                if (!_playerPrefsManager.HasKey(TOTAL_SESSIONS_TIME_KEY))
                    return 0;

                var rawTotalTime = _playerPrefsManager.GetValue<int>(TOTAL_SESSIONS_TIME_KEY);

                var totalTime = TimeSpan.FromSeconds(rawTotalTime);
                return (int)totalTime.TotalMinutes;
            }
        }

        public int TotalPlayTimeSec
        {
            get
            {
                UpdateSessionTime();

                if (!_playerPrefsManager.HasKey(TOTAL_SESSIONS_TIME_KEY))
                    return 0;

                return _playerPrefsManager.GetValue<int>(TOTAL_SESSIONS_TIME_KEY);
            }
        }

        public int DaysInGame
        {
            get
            {
                var playDays = _playerPrefsManager.HasKey(DAYS_IN_GAME_KEY)
                    ? _playerPrefsManager.GetValue<int>(DAYS_IN_GAME_KEY)
                    : 0;

                var now = DateTimeOffset.Now;
                var nowUnix = now.ToUnixTimeSeconds();

                if (long.TryParse(_playerPrefsManager.GetValue(NEXT_DAY_KEY, "0"), out var lastDay)
                 && lastDay < nowUnix)
                {
                    playDays++;
                    _playerPrefsManager.SetValue(DAYS_IN_GAME_KEY, playDays);

                    var nextDay = now.AddDays(1);
                    var nextDayUnix = nextDay.ToUnixTimeSeconds();
                    _playerPrefsManager.SetValue(NEXT_DAY_KEY, nextDayUnix.ToString());

                    _playerPrefsManager.Save();
                }

                return playDays;
            }
        }

        public void Initialize()
        {
            var now = DateTimeOffset.Now;
            var nowUnix = now.ToUnixTimeSeconds();
            _playerPrefsManager.SetValue(SESSION_STAMP_KEY, nowUnix.ToString());
            _playerPrefsManager.Save();
        }

        public void IncreaseRestartCount()
        {
            RestartCount++;
            var totalRestartCount = TotalRestartCount + 1;
            _playerPrefsManager.SetValue(TOTAL_RESTART_COUNT_KEY, totalRestartCount);
            _playerPrefsManager.Save();
            
            LevelTransitionsInRunCount = 0;
        }

        public void IncreaseTransitionsCount()
        {
            LevelTransitionsInRunCount++;
            _playerPrefsManager.SetValue(TOTAL_LEVEL_TRANSITIONS_COUNT_KEY, TotalLevelTransitionsCount + 1);
            _playerPrefsManager.Save();
        }

        private void UpdateSessionTime()
        {
            var now = DateTimeOffset.Now;
            var nowUnix = now.ToUnixTimeSeconds();

            var rawLastStamp = _playerPrefsManager.GetValue(SESSION_STAMP_KEY, nowUnix.ToString());

            if (!long.TryParse(rawLastStamp, out var lastStamp))
                return;

            var diff = nowUnix - lastStamp;

            var totalTime = _playerPrefsManager.GetValue(TOTAL_SESSIONS_TIME_KEY, 0);
            totalTime += (int)diff;

            _playerPrefsManager.SetValue(TOTAL_SESSIONS_TIME_KEY, totalTime);
            _playerPrefsManager.SetValue(SESSION_STAMP_KEY, nowUnix.ToString());
            _playerPrefsManager.Save();
        }

        public void Tick()
        {
            _accumulator += Time.deltaTime;

            if (_accumulator >= AUTO_SESSION_REFRESH_S)
            {
                UpdateSessionTime();
                _accumulator = 0;
            }
        }

        void IDisposable.Dispose()
        {
            UpdateSessionTime();
        }
    }
}