using System;
using DG.Tweening;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Project.Ui.Loading.Views
{
    public class ReloadingView : UiView
    {
        private const float ALPHA_SHOW_VALUE = 1f;
        private const float ALPHA_HIDE_VALUE = 0f;
        
        [SerializeField] private CanvasGroup _fadeTarget;
        
        [Header("Animation Parameters")]
        [SerializeField] private float _fadeAnimationDuration = 0.5f;
        [SerializeField] private Ease _fadeAnimationEase = Ease.Linear;

        public void PlayFadeAnimation(Action onComplete)
        {
            // Более показать не могу
        }

        private void ResetView() {
            // Более показать не могу
        }
    }
}