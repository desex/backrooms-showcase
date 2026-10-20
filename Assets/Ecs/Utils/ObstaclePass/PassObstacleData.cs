using UnityEngine;

namespace Ecs.Utils.ObstaclePass
{
    public readonly struct PassObstacleData
    {
        public readonly float ComboMoveMultiplier;
        public readonly PassObstacleMovePointsData[] MovePoints;
        public readonly Vector3 CenterStartReadyTriggerPosition;

        public PassObstacleData(
            float comboMoveMultiplier, 
            PassObstacleMovePointsData[] movePoints, 
            Vector3 centerStartReadyTriggerPosition
        )
        {
            ComboMoveMultiplier = comboMoveMultiplier;
            MovePoints = movePoints;
            CenterStartReadyTriggerPosition = centerStartReadyTriggerPosition;
        }
    }
}