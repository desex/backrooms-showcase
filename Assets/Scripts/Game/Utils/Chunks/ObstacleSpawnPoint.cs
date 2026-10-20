using System;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct ObstacleSpawnPoint
    {
        public Transform Point;
        public float LeftOffset;
        public float RightOffset;
    }

}