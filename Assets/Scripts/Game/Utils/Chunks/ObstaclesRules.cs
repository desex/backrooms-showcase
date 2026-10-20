using System;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct ObstaclesRules
    {
        public int DistanceFor4Obstacles;
        public int DistanceFor5Obstacles;
        
        public Vector2Int DistanceRange 
        {
            get => new Vector2Int(DistanceFor4Obstacles, DistanceFor5Obstacles);
            set => (DistanceFor4Obstacles, DistanceFor5Obstacles) = (value.x, value.y);
        }
    }
}