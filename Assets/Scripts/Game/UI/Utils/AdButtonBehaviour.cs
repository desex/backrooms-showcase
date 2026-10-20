using DG.Tweening;
using UnityEngine;

namespace Game.UI.Utils
{
    public class AdButtonBehaviour : OncePressedButton
    {
        public GameObject enableState;
        public GameObject disableState;
        
        [SerializeField] private Transform spinner;
        [SerializeField] private float spinnerSpeed;

        private Tweener _spinnerTweener;

        public void Unlock()
        {
            // Более показать не могу
        }

        public void Lock()
        {
            // Более показать не могу
        }

        private void CreateSpinner()
        {
            // Более показать не могу
        }
    }
}