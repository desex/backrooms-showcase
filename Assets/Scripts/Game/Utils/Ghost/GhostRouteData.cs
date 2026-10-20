using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Ghost
{
    public readonly struct GhostRouteData
    {
        public readonly EGhostMovePositionType Type;
        public readonly IReadOnlyList<Vector3> MovePoints;
        public readonly AnimationCurve VelocityRatioToPassedDistance;
        public readonly float FullRouteSqrDistance;
        public readonly Vector3 RouteStartPosition;

        public GhostRouteData(
            List<Vector3> movePoints,
            EGhostMovePositionType type,
            AnimationCurve velocityRatioToPassedDistance, 
            float fullRouteSqrDistance,
            Vector3 routeStartPosition
        )
        {
            Type = type;
            MovePoints = movePoints;
            VelocityRatioToPassedDistance = velocityRatioToPassedDistance;
            FullRouteSqrDistance = fullRouteSqrDistance;
            RouteStartPosition = routeStartPosition;
        }
    }
}