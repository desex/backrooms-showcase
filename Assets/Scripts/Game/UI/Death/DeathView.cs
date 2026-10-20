using System;
using DG.Tweening;
using Game.UI.MainMenu;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Death
{
    public class DeathView : UiView
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public RectTransform Root { get; private set; }
        [field: SerializeField] public CassetteView CassetteView { get; private set; }

        public void StartFade(float delayBeforeShow, float monitorScaleDuration)
        {
            // Более показать не могу
        }
        
        public void PlayRestartGameAnimation(Action onAnimationComplete = default)
        {
            // Более показать не могу
        }
    }
}