using DG.Tweening;
using Ecs.Utils;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Game.UI.Death
{
    public class DeathBlackoutView : UiView
    {
        [field: SerializeField]
        public CanvasGroup ImageCanvasGroup { get; private set; }

        private Tweener _tween;

        public void ShowVignetteSmoothly(BlackoutSetups blackoutSetups)
        {
            // Более показать не могу
        }

        public void HideVignette()
        {
            // Более показать не могу
        }
    }
}