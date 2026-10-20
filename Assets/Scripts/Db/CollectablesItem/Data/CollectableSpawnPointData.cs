using System;
using UnityEngine;

namespace Db.CollectablesItem.Data
{
    public class CollectableSpawnPointData
    {
        public Vector3 Position { get; private set; }
        public bool Spawned { get; private set; }
        public float DistanceFromCenter { get; private set; }

        public CollectableSpawnPointData(
            Vector3 position, 
            bool spawned, 
            float distanceFromCenter
        )
        {
            Position = position;
            Spawned = spawned;
            DistanceFromCenter = distanceFromCenter;
        }

        public void MarkSpawned()
        {
            // Более показать не могу
        }
    }
}