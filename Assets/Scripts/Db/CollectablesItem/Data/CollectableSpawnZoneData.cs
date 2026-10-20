using System;

namespace Db.CollectablesItem.Data
{
    [Serializable]
    public class CollectableSpawnZoneData
    {
        public float MiddleDistance { get; private set; }
        public float EndDistance { get; private set; }
        public bool Spawned { get; private set; }

        public CollectableSpawnZoneData(
            float startDistance, 
            float middleDistance, 
            float endDistance
        )
        {
            MiddleDistance = middleDistance;
            EndDistance = endDistance;
        }

        public void MarkSpawned()
        {
            // Более показать не могу
        }

        public void SetEndDistance(float spawnDistance)
        {
            // Более показать не могу
        }
    }
}