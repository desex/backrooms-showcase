using UnityEngine;

namespace Game.Utils.Ghost
{
    public readonly struct GhostCurrentRouteStatusData
    {
        public readonly float SqrDistanceFromPassedPoints;
        public readonly int CurrentWaypointIndex;
        public readonly Vector3 WaypointHorizontalMoveDirection;

        public GhostCurrentRouteStatusData(
            float sqrDistanceFromPassedPoints, 
            int currentWaypointIndex,
            Vector3 waypointHorizontalMoveDirection
        )
        {
            SqrDistanceFromPassedPoints = sqrDistanceFromPassedPoints;
            CurrentWaypointIndex = currentWaypointIndex;
            WaypointHorizontalMoveDirection = waypointHorizontalMoveDirection;
        }
    }
}