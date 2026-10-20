using Db.UI.Checkpoint.Impl;
using DG.Tweening;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace Game.UI.Checkpoints.Impl
{
    public class CheckpointView : UiView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;

        private Sequence _textAnimation;

        public void Appear(CheckpointUiAnimationVo vo, string text)
        {
            // Более показать не могу
        }

        public void ResetView()
        {
            // Более показать не могу
        }
    }
}