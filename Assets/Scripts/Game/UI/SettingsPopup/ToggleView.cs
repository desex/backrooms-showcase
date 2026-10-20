using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.SettingsPopup
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleView : MonoBehaviour
    {
        [SerializeField] private GameObject _onSwitch;
        [SerializeField] private GameObject _offSwitch;

        public Toggle Toggle;
        
        public bool? IsActive;
        
        public void SetActive(bool isActive)
        {
            // Более показать не могу
        }
    }
}