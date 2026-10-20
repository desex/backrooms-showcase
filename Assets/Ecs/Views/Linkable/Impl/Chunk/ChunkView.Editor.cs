#if UNITY_EDITOR
using Core.Utils;
using Ecs.Views.Linkable.Impl.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Chunk
{
    public partial class ChunkView
    {
        [SerializeField] private bool _isDrawCollisionBounds;
        
        private void OnDrawGizmos()
        {
            // Более показать не могу
        }

        private bool ValidateChunkLength(int _)
        {
            // Более показать не могу
            return default;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
        
        [Button]
        public void FillObstacles()
        {
            // Более показать не могу
        }

        [Button]
        private void SetSizeValuesFromCollider(int index, BoxCollider collisionCollider)
        {
            // Более показать не могу
        }
    }
}
#endif