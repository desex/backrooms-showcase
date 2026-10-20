using DG.Tweening;
using Game.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.UI.Tutorial.Hints
{
    public sealed class TiltingTutorialHint : ATutorialHintView
    {
        [Title("Animation targets")]
        [SerializeField] private RectTransform _arrowTarget;

        [Title("Animation parameters")]
        [SerializeField] private float _maxXPosition;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Sequence _sequence;

        protected override void DoShow()
        {
            // Более показать не могу
        }

        protected override void DoHide()
        {
            // Более показать не могу
        }

        private void ResetAnimation()
        {
            // Более показать не могу
        }
    }
}