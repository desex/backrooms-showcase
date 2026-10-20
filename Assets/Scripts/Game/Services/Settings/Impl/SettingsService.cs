using Db.Settings;
using Game.DaoData;
using PdUtils.Dao;
using R3;
using UnityEngine;

namespace Game.Services.Settings.Impl
{
    public class SettingsService : ISettingsService
    {
        private readonly ReactiveProperty<bool> _isNotVolumeMuted = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _isVibrationEnabled = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<float> _sensitivity = new ReactiveProperty<float>();
        private readonly IDao<SettingsData> _settingsDao;
        private readonly ISettingsParameter _settingsParameter;

        private SettingsData _settingsData;

        public SettingsService(IDao<SettingsData> settingsDao, ISettingsParameter settingsParameter)
        {
            _settingsDao = settingsDao;
            _settingsParameter = settingsParameter;
            InitData();
        }

        public ReadOnlyReactiveProperty<bool> IsNotVolumeMuted => _isNotVolumeMuted;
        public ReadOnlyReactiveProperty<float> Sensitivity => _sensitivity;
        public ReadOnlyReactiveProperty<bool> IsVibrationEnabled => _isVibrationEnabled;

        public void Mute()
        {
            // Более показать не могу
        }

        public void Unmute()
        {
            // Более показать не могу
        }

        public void EnableVibration()
        {
            // Более показать не могу
        }

        public void DisableVibration()
        {
            // Более показать не могу
        }

        public void SetSensitivity(float sensitivity)
        {
            // Более показать не могу
        }

        private void InitObservables()
        {
            // Более показать не могу
        }

        private void InitData()
        {
            // Более показать не могу
        }
    }
}