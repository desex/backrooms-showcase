using System;
using Db.Fade.Impl;
using DG.Tweening;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.SettingsPopup
{
    public class SettingsPopupView : UiView
    {
        public RectTransform SettingsRect;
        
        [Header("Switches")]
        public ToggleView VibrationToggle;
        public ToggleView SoundToggle;
        public ToggleView PushToggle;
        
        [Header("Fade")]
        public Image FadeImage;
        
        [Header("Buttons")]
        public Button CloseButton;
        public Button PrivacyButton;
        public Button TermsOfServiceButton;
        public Button LicenceButton;
        public Button BackgroundButton;
        public Button CopyDebugInfoButton;
        
        private Sequence _fadeSequence;
        
        [field: Header("Time")]
        [field: SerializeField] public float TimeToShowSettings { get; private set; }
        
        [field: Header("Sensitivity")]
        [field: SerializeField] public Slider SensitivitySlider { get; private set; }
        
        public void FadeIn(FadeVo fadeVo, Action onFadeComplete = null)
        {
            // Более показать не могу
        }

        public void FadeOut(FadeVo fadeVo, Action onFadeComplete = null)
        {
            // Более показать не могу
        }
    }
}