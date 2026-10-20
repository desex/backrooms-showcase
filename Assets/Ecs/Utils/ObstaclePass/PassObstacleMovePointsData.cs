using System;
using UnityEngine;

namespace Ecs.Utils.ObstaclePass
{
    [Serializable]
    public struct PassObstacleMovePointsData
    {
        public EPassObstacleType PassObstacleType;
        public PassObstacleMovePoint[] MovePoints;
        [Range(0.1f, 3)] public float PassObstacleAnimationSpeedMultiplayer;
    }
}