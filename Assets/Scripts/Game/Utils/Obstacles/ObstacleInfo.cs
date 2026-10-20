using System;
using System.Collections;
using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Obstacles;

namespace Game.Utils.Obstacles
{
    [Serializable]
    public sealed class ObstacleTypeInfo : IReadOnlyList<ObstacleGroupView>
    {
        public bool IsObstacleWithWalls;
        public ObstacleGroupView[] Obstacles;

        public int Count => Obstacles.Length;

        public ObstacleGroupView this[int index] => Obstacles[index];
        
        public IEnumerator<ObstacleGroupView> GetEnumerator()
        {
            // Более показать не могу
            return ((global::System.Collections.Generic.IEnumerable<ObstacleGroupView>) global::System.Array.Empty<ObstacleGroupView>()).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Более показать не могу
            return default;
        }
    }
}