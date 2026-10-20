using System;
using DG.Tweening;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Game.UI.Tutorial
{
    public class TutorialFailView : UiView
    {
        private const float ALPHA_HIDE_VALUE = 0f;
        private const float ALPHA_SHOW_VALUE = 1f;
        
        [SerializeField] private float _fadeInDuration = 1f;
        [SerializeField] private Ease _fadeInEase;
        
        [SerializeField] private float _fadeDuration = 1f;
        
        [SerializeField] private float _fadeOutDuration = 1f;
        [SerializeField] private Ease _fadeOutEase;

        [SerializeField] private CanvasGroup _canvasGroupForFade;

        private Sequence _fadeSequence;

        public void ResetFade()
        {
            // Более показать не могу
        }

        public void PlayFadeAnimation(Action onFadeOutStart, Action onFadeOutComplete)
        {
            // Более показать не могу
        }
    }
}