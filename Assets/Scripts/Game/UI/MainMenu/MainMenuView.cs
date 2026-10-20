using System;
using Db.MainMenu;
using DG.Tweening;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.MainMenu
{
    public class MainMenuView : UiView
    {
        [Inject] private readonly IMainMenuAnimationParameters _animationParameters;
        
        [field: Header("Root")]
        [field: SerializeField] public RectTransform Root { get; private set; }
        
        [field: Space]
        [field: Header("Buttons")]
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button PlayButton { get; private set; }
         
        [field: Space]
        [field: Header("Cassette")]
        [field: SerializeField] public CassetteView CassetteView { get; private set; }
        
        [field: Space]
        [field: Header("Monitor")]
        [field: SerializeField] public MonitorView MonitorView { get; private set; }

        public void StartScreenAnimation(float duration)
        {
            // Более показать не могу
        }
        
        public void PlayStartGameplayAnimation(Action onAnimationComplete = null, Action onPlayScaleAnimation = null, bool animateCassette = true)
        {
            // Более показать не могу
        }
        
        public void EnterGameplayWithoutAnimation(Action onAnimationComplete = null, Action onPlayScaleAnimation = null, bool animateCassette = true)
        {
            // Более показать не могу
        }
        
        public void ResetView()
        {
            // Более показать не могу
        }

        public void SkipToInsertedStage()
        {
            // Более показать не могу
        }

        public void ResetScaleMonitor()
        {
            // Более показать не могу
        }
    }
}