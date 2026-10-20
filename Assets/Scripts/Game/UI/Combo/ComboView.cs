using System;
using Db.UI.Combo.Impl;
using DG.Tweening;
using Game.Services.General;
using Game.UI.UIBase;
using SimpleUi.Abstracts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Combo
{
    public class ComboView : UiView, IUiDisablable
    {
        [field: Title("Parameters")]
        [field: SerializeField] public ComboViewParameters Parameters { private set; get; }

        [field: Title("Combo")]
        [field: SerializeField] public Image ComboSmokeImage { private set; get; }
        [field: SerializeField] public RectTransform ComboContainer { private set; get; }
        
        [field: Title("Combo Counter")]
        [field: SerializeField] public TextMeshProUGUI ComboCountText { private set; get; }
        [field: SerializeField] public RectTransform ComboCountContainer { private set; get; }
        
        [field: Title("Combo Slider")]
        [field: SerializeField] public Image ComboSlider { private set; get; }
        [field: SerializeField] public Image ComboSliderImage { private set; get; }
        
        [field: Title("Combo Text")]
        [field: SerializeField] public TextMeshProUGUI ComboText { private set; get; }
        [field: SerializeField] public RectTransform ComboTextContainer { private set; get; }
        [field: SerializeField] public CanvasGroup ComboTextCanvasGroup { private set; get; }
        
        [field: Title("Bonus Water")]
        [field: SerializeField] public RectTransform BonusWaterContainer { private set; get; }
        
        private Sequence _comboAnimation;
        private Sequence _comboWordsAnimation;
        private Sequence _waterAnimation;
        
        private bool _isAppeared;
        
        public void AppearCombo()
        {
            // Более показать не могу
        }

        public void ReappearCombo()
        {
            // Более показать не могу
        }
        
        public void DisappearCombo()
        {
            // Более показать не могу
        }
        
        //TODO на будущее: при наборе комбо на второй бонус за ( < Parameters.WaterBonusDuration ) надо поменять текст с +1 на +2
        public void AppearWaterBonus(Action addAlmondWaterOnCombo)
        {
            // Более показать не могу
        }

        public void AppearComboText(string text)
        {
            // Более показать не могу
        }

        private void DisappearComboText()
        {
            // Более показать не могу
        }
        
        public void ResetView()
        {
            // Более показать не могу
        }

        private static void ResetContainerPosition(RectTransform container, float xPosition)
        {
            // Более показать не могу
        }
    }
}