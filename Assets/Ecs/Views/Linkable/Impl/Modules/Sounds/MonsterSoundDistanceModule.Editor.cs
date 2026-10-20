using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Sounds
{
    public partial class MonsterSoundDistanceModule
    {
#if UNITY_EDITOR
        [Header("Editor")]
        [SerializeField] private float _radius = 0.3f;
        [SerializeField] private Color _color = Color.red;
        
        private void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
#endif
    }
}