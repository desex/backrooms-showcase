using Db.Fade.Impl;
using DG.Tweening;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace Game.UI.FinalScore
{
    public class FinalScoreView : UiView
    {
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PassedDistanceText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI BestPassedDistanceText { get; private set; }
        [field: SerializeField] public CanvasGroup FadeTarget { get; private set; }

        public void StartFade(FadeVo fadeVo)
        {
            // Более показать не могу
        }
    }
}