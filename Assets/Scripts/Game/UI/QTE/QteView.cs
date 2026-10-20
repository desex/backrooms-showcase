using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.QTE
{
    public class QteView : UiView
    {
        [field: SerializeField] public Image ProgressCircle { get; private set; }
        [field: SerializeField] public Image TimerCircle { get; private set; }
        [field: SerializeField] public CanvasGroup TapText { get; private set; }
    }
}