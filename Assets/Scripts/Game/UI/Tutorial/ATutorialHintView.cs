using UnityEngine;

namespace Game.UI.Tutorial
{
    public abstract class ATutorialHintView : MonoBehaviour
    {
        public void Show()
        {
            // Более показать не могу
        }

        public void Hide()
        {
            // Более показать не могу
        }

        protected abstract void DoShow();
        protected abstract void DoHide();
    }
}