using Game.Utils;
using Game.Utils.ColorsUtils;
using Game.Utils.SerializeCollection.Impl.Dictionary;
using Game.Utils.Tutorial;
using SimpleUi.Abstracts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.UI.Tutorial
{
    public class TutorialHintsView : UiView
    {
        [SerializeField]
        [TableList]
        private DictionaryHintsByTutorialState _hintViews;

        public void Initialize()
        {
            // Более показать не могу
        }
        
        public void ShowHint(ETutorialHint hint)
        {
            // Более показать не могу
        }

        public void HideHint(ETutorialHint hint)
        {
            // Более показать не могу
        }

        public void HideAllHints()
        {
            // Более показать не могу
        }

        private bool TryGetHint(ETutorialHint hint, out ATutorialHintView hintView)
        {
            // Более показать не могу
            hintView = default;
            return default;
        }
    }
}