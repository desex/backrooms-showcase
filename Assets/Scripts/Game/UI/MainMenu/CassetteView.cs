using System;
using Db.MainMenu;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.MainMenu
{
    public class CassetteView : MonoBehaviour
    {
        [Inject] private readonly IMainMenuAnimationParameters _mainMenuParameters;
        
        [Header("Cassette")]
        [SerializeField] private RectTransform _cassetteTransform;

        [Header("Curtain")]
        [SerializeField] private RectTransform _curtainTransform;

        [Header("Fade")]
        [SerializeField] private Image _fade;

        public Tween PlayInsertAnimation(Action onComplete = null)
        {
            // Более показать не могу
            return default;
        }

        public void ResetView()
        {
            // Более показать не могу
        }

        public void SkipToInsertedStage()
        {
            // Более показать не могу
        }
    }
}