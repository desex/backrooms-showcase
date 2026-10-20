using Ecs.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class ObstacleView : PoolObjectView
    {
        [field: SerializeField]
        private AObjectViewModule _drawModule;
        
#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
#endif
    }
}