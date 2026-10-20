using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Obstacles;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Chunk
{
    public class ChunkWallView : PoolObjectView
    {
        [HideInInspector] public GameObject Lamps;
        
        [field: SerializeField] public Transform EndAnchor { get; private set; }
        [field: SerializeField] public List<ObstacleGroupView> BuiltInObstacles { get; private set; }
        
        public ReactiveProperty<bool> IsMirrored { get; } = new ();
        
        [Button]
        public void FillObstacles()
        {
            // Более показать не могу
        }

        public void SetMirroredState(bool state)
        {
            // Более показать не могу
        }
    }
}