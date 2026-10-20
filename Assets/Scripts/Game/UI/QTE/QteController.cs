using System;
using Db.QTE;
using DG.Tweening;
using PdUtils.Interfaces;
using R3;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Game.UI.QTE
{
    public class QteController : UiController<QteView>, IDisposable, IUiInitializable
    {
        private readonly GameContext _game;
        private readonly IQteParameters _qteParameters;

        private Sequence _blinkSequence;
        private Tween _tapProgressTween;

        public QteController(GameContext game, IQteParameters qteParameters)
        {
            _game = game;
            _qteParameters = qteParameters;
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        private void OnTimerChanged(GameEntity entity, float newProgress)
        {
            // Более показать не могу
        }

        private void OnLeftEscapeChanged(GameEntity entity, int newProgress)
        {
            // Более показать не могу
        }

        private void AnimateText()
        {
            // Более показать не могу
        }

        public override void OnHide()
        {
            // Более показать не могу
        }
    }
}