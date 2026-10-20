#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using TMPro;
using UnityEngine;

namespace Game.UI.GameRestartButton
{
    public class FPSView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentFPSText;
        [SerializeField] private TextMeshProUGUI _averageFPSText;
        [SerializeField] private float _betweenUpdateCurrentTime;
        [SerializeField] private float _betweenUpdateAverageTime;

        private float _currentFPS = 60;
        private float _averageFPS = 60;

        private float _currentFPSTime;
        private float _averageFPSTime;

        private int _totalFrames;
        private float _fpsSum;

        private string _currentFSPTextLabel = "FPS";
        private string _averageFSPTextLabel = "Avg FPS";

        private void Update()
        {
            // Более показать не могу
        }

        private void UpdateCurrentFPS()
        {
            // Более показать не могу
        }

        private void UpdateAverageFPS()
        {
            // Более показать не могу
        }
    }
}
#endif