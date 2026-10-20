using DG.Tweening;
using Game.UI.UIBase;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.ChaseProgressBar
{
    public class ChaserView : UiView, IUiDisablable
    {
        [SerializeField] private Slider _progressBar;
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
        
        private Tween _progressBarAnimation;
        
        public void SetProgress(float progress, float duration, Ease ease)
        {
            // Более показать не могу
        }

        public void SetProgress(float progress)
        {
            // Более показать не могу
        }
        
        public float ChaserProgress => _progressBar.value; 
    }
}