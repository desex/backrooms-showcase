using UnityEngine;

namespace Game.Services.Effects
{
    public class DestructableEffect : MonoBehaviour
    {
        [field: SerializeField]
        public ParticleSystem ParticleSystem { get; private set; }

        public void Show()
        {
            // Более показать не могу
        }

        public void Hide()
        {
            // Более показать не могу
        }
    }
}