using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Revive
{
    public partial class ReviveModule
    {
#if UNITY_EDITOR
        [Title("Editor")]
        [SerializeField] private Color _sphereColor = Color.magenta;
        [SerializeField] private float _sphereRadius = 0.15f;

        private void OnDrawGizmos()
        {
            // Более показать не могу
        }
#endif
    }
}