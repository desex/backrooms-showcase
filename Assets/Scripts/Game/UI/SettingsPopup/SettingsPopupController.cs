using System;
using Db.Fade;
using Db.Fade.Impl;
using Db.Settings;
using Db.SoundFxBase;
using DG.Tweening;
using Game.Services;
using Game.Services.Settings;
using Game.Services.Sound;
using PdUtils.Interfaces;
using R3;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

namespace Game.UI.SettingsPopup
{
    public class SettingsPopupController : UiController<SettingsPopupView>, IUiInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly ISoundService _soundService;
        private readonly IFadeParameters _fadeParameters;
        private readonly ISettingsParameter _settingsParameter;
        private readonly ISettingsService _settingsService;

        private readonly ITextLoggerService _logger;

        private Vector2 _hidePosition;

        public SettingsPopupController(
            SignalBus signalBus,
            ISoundService soundService,
            IFadeParameters fadeParameters,
            ISettingsParameter settingsParameter,
            ISettingsService settingsService,
            ITextLoggerService logger
        )
        {
            _signalBus = signalBus;
            _soundService = soundService;
            _fadeParameters = fadeParameters;
            _settingsParameter = settingsParameter;
            _settingsService = settingsService;
            _logger = logger;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        private void SetMinMaxSliderValues()
        {
            // Более показать не могу
        }

        private void OnVibrationChange(bool isOn)
        {
            // Более показать не могу
        }

        private void OnSoundChange(bool isOn)
        {
            // Более показать не могу
        }

        private void OnClose()
        {
            // Более показать не могу
        }

        private void OnCopyDebugInfo()
        {
            // Более показать не могу
        }

        private void AnimateSettingsWindow(bool isShow, Action onComplete = null)
        {
            // Более показать не могу
        }

        private void ResetViewPosition()
        {
            // Более показать не могу
        }

        private void LoadSettings()
        {
            // Более показать не могу
        }

        private void SetVibrationState(bool isVibrationEnable)
        {
            // Более показать не могу
        }

        private void SetSoundState(bool isSoundMuted)
        {
            // Более показать не могу
        }
    }
}