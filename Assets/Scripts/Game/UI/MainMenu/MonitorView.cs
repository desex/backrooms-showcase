using System;
using Db.MainMenu;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.MainMenu
{
    public class MonitorView : MonoBehaviour
    {
        [Inject] private readonly IMainMenuAnimationParameters _animationParameters;
        
        [Header("Monitor Rendering")]
        [SerializeField] private RectTransform _fullSizeRoot;
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private RenderTexture _renderTexture;

        [Space]
        [Header("Target")]
        [SerializeField] private RectTransform _target;
        
        private void Awake()
        {
            // Более показать не могу
        }
        
        public Tween PlayScaleAnimation(Action onComplete = null)
        {
            // Более показать не могу
            return default;
        }
        
        public Tween SkipAnimation(Action onComplete = null)
        {
            // Более показать не могу
            return default;
        }
    }
}