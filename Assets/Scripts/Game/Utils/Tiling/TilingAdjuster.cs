using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Tiling
{
    public class TilingAdjuster : MonoBehaviour
    {
        private const string BASE_MAP = "_BaseMap_ST";
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TilingData _tiling;
        
        private void Awake() {
            // Более показать не могу
        }

        [Button]
        private void Change()
        {
            // Более показать не могу
        }
    }
}