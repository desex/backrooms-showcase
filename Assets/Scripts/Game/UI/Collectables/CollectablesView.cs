using Game.UI.UIBase;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace Game.UI.Collectables
{
    public class CollectablesView : UiView, IUiDisablable
    {
        public CanvasGroup CassetteView;
        public CanvasGroup CassetteViewText;
        
        [Space]
        
        public CanvasGroup BacteriaView;
        public CanvasGroup BacteriaViewText;
        
        [Space] 
        
        public TMP_Text CassetteText;
        public TMP_Text BacteriaText;
        public TMP_Text AlmondWaterText;
    }
}