using Game.UI.UIBase;
using SimpleUi.Abstracts;
using TMPro;

namespace Game.UI.Score
{
    public class ScoreView : UiView, IUiDisablable
    {
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI PassedDistanceText;
    }
}