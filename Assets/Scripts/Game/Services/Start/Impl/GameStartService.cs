using System;
using System.Collections.Generic;
using PdUtils.FirstStartService;
using PdUtils.PlayerPrefs;
using R3;
using Zenject;

namespace Game.Services.Start.Impl
{
    public class GameStartService : IGameStartService, IInitializable
    {
        private const string KEY_FIRST_APPLICATION_LAUNCH = "FirstApplicationLaunch";
        private const string KEY_FIRST_LAUNCH_TIMESTAMP = "FirstLauchTimestamp";
        private const string SESSION_SAVE_KEY = "SessionNumber";

        private readonly IFirstStartService _firstStartService;

        private readonly ReactiveProperty<bool> _isTransitingToAnotherLevelProperty = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _needSkipMainMenuProperty = new ReactiveProperty<bool>(false);
        private readonly IPlayerPrefsManager _playerPrefsManager;
        private readonly IYandexAppMetrica _yandexAppMetrica;

        public GameStartService(
            IFirstStartService firstStartService,
            IPlayerPrefsManager playerPrefsManager,
            IYandexAppMetrica yandexAppMetrica
        )
        {
            _firstStartService = firstStartService;
            _playerPrefsManager = playerPrefsManager;
            _yandexAppMetrica = yandexAppMetrica;
        }

        public int DaysSinceReg
        {
            get
            {
                var firstStartTimeUtc = _firstStartService.GetFirstStartTimeUtc();
                return firstStartTimeUtc.HasValue ? (DateTime.UtcNow - firstStartTimeUtc.Value).Days : 0;
            }
        }

        public ReadOnlyReactiveProperty<bool> NeedSkipMainMenu => _needSkipMainMenuProperty;
        public ReadOnlyReactiveProperty<bool> IsTransitingToAnotherLevel => _isTransitingToAnotherLevelProperty;

        public void SetSkipMainMenu(bool needSkipMainMenu)
        {
            // Более показать не могу
        }

        public int SessionNumber
        {
            get => _playerPrefsManager.GetValue(SESSION_SAVE_KEY, 0);

            private set
            {
                _playerPrefsManager.SetValue(SESSION_SAVE_KEY, value);
                _playerPrefsManager.Save();
            }
        }

        public int ApplicationLaunchCount
        {
            get => _playerPrefsManager.GetValue(KEY_FIRST_APPLICATION_LAUNCH, 0);

            private set
            {
                _playerPrefsManager.SetValue(KEY_FIRST_APPLICATION_LAUNCH, value);
                _playerPrefsManager.Save();
            }
        }

        public long FirstLaunchTimestamp
        {
            get
            {
                if (!_playerPrefsManager.HasKey(KEY_FIRST_LAUNCH_TIMESTAMP))
                {
                    var dateTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    _playerPrefsManager.SetValue(KEY_FIRST_LAUNCH_TIMESTAMP, dateTime);
                }

                return _playerPrefsManager.GetValue<long>(KEY_FIRST_LAUNCH_TIMESTAMP);
            }
        }

        public void SetTransitionFlag(bool isTransitioning)
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }
    }
}