using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Utils
{
    [RequireComponent(typeof(Button))]
    public class OncePressedButton : MonoBehaviour
    {
        private readonly ReactiveCommand _onPressCommand = new();
        
        private Button _button;

        [ShowInInspector]
        [ReadOnly]
        private bool _wasPressed;
        
        public Observable<Unit> OnPress => _onPressCommand;
        
        private void Awake()
        {
            // Более показать не могу
        }

        public void SetButtonInteractable(bool value)
        {
            // Более показать не могу
        }

        private void OnClick()
        {
            // Более показать не могу
        }

        private void OnDestroy()
        {
            // Более показать не могу
        }
        
        public void ResetPressState()
        {
            // Более показать не могу
        }
    }
}