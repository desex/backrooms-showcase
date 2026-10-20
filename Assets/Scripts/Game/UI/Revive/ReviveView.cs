using System;
using Db.ReviveView.Impl;
using DG.Tweening;
using Game.UI.Utils;
using SimpleUi.Abstracts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Revive
{
    public class ReviveView : UiView
    {
        [field: Space(10)]
        [field: Header("Counter")]
        [SerializeField]
        private Image _counterImage;
        
        [SerializeField]
        private TMP_Text _counterText;
        
        [SerializeField]
        private TMP_Text _counterBottles;
        
        [SerializeField]
        private TMP_Text _messageBottlesText;
        
        [field: SerializeField]
        public Button CloseButton { get; private set; }
        
        [field: SerializeField]
        public Button ReviveForWaterButton { get; private set; }
        
        [field: SerializeField]
        public AdButtonBehaviour ReviveForAdButton { get; private set; }
        
        [field: SerializeField]
        public TMP_Text RemainingDistanceText { get; private set; }
        
        [field: Space(10)]
        [field: Header("Almond water for revive")]
        [field: SerializeField]
        private RectTransform _containerBackgroundBottle;
        
        [field: Title("Parameters")]
        [field: SerializeField]
        public ReviveViewSettings Parameters { get; private set; }
        
        public Tween StartCount(int counterSeconds, Action onComplete = default)
        {
            // Более показать не могу
            return default;
        }
        
        public void UpdateRemainingDistanceText(int currentPassedDistance, int bestDistance)
        {
            // Более показать не могу
        }
        
        public void UpdateCounterBottlesText(int collectiblesStorageItems)
        {
            // Более показать не могу
        }
        
        public void AnimationBottle()
        {
            // Более показать не могу
        }
        
        private void AddBottlePulse(Sequence sequence, float targetScale, float duration)
        {
            // Более показать не могу
        }
    }
}