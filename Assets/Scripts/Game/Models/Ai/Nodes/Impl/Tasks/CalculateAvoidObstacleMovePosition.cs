using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using Game.Utils.Ghost;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class CalculateAvoidObstacleMovePosition : AEntityAction
    {
        private IRandomProvider _randomProvider;
        private IGhostsParameters _ghostsParameters;

        [Inject]
        private void Construct(
            IGhostsParameters ghostsParameters, 
            IRandomProvider randomProvider
        )
        {
            _ghostsParameters = ghostsParameters;
            _randomProvider = randomProvider;
        }

        public override TaskStatus OnUpdate()
        {
            var ghost = GetEntity();
            var avoidanceZonesData = ghost.GhostAvoidanceZones.Value;

            var ghostPosition = ghost.Position.Value;

            GhostSafeZonePositionsData? closestSafeZone = null;
            var closestSafeZoneSqrDistance = float.MaxValue;

            var ghostsWidth = _ghostsParameters.GhostWidth;

            if (avoidanceZonesData.SafeZones == null)
                return TaskStatus.Success;
            
            foreach (var safeZonePositionsData in avoidanceZonesData.SafeZones)
            {
                var firstPoint = safeZonePositionsData.LeftPoint;
                var secondPoint = safeZonePositionsData.RightPoint;
                var zoneSqrLenght = (firstPoint - secondPoint).sqrMagnitude;
                
                if (zoneSqrLenght <= ghostsWidth * ghostsWidth)
                    continue;
                
                if (!TryUpdateClosestPoint(safeZonePositionsData.LeftPoint)
                 && !TryUpdateClosestPoint(safeZonePositionsData.RightPoint))
                    continue;
                
                closestSafeZone = safeZonePositionsData;
            }

            if (!closestSafeZone.HasValue)
                return TaskStatus.Failure;

            var selectedPoint = SelectRandomPointBetweenPoints(closestSafeZone.Value);

            var movePoints = new List<Vector3>
            {
                selectedPoint,
            };

            var moveVector = (selectedPoint - ghostPosition);
            var totalSqrDistance = moveVector.sqrMagnitude;
            var avoidObstaclesCurve = _ghostsParameters.AvoidObstacleVelocityRatioToPassedDistanceCurve;
            var moveData = new GhostRouteData(movePoints,
                                              EGhostMovePositionType.AvoidObstacle,
                                              avoidObstaclesCurve,
                                              totalSqrDistance,
                                              ghostPosition
            );
            
            var ghostRightDirection = ghost.Transform.Value.right;
            var horizontalMoveDirection = Vector3.Project(moveVector, ghostRightDirection);
            ghost.ReplaceGhostRouteData(moveData);
            ghost.ReplaceGhostCurrentRouteStatus(new(0f, 0, horizontalMoveDirection));

            return TaskStatus.Success;

            Vector3 SelectRandomPointBetweenPoints(GhostSafeZonePositionsData zonePositionsData)
            {
                var firstPoint = zonePositionsData.LeftPoint;
                var secondPoint = closestSafeZone.Value.RightPoint;
                var pointsVector = secondPoint - firstPoint;
                var pointsVectorLength = pointsVector.magnitude;
                var normalizedPointsVector = pointsVector / pointsVectorLength;
                
                var ghostWidth = ghostsWidth;
                var ghostHalfWidth = ghostWidth / 2;
                var randomLengthFromLeftBorder = _randomProvider.Range(ghostHalfWidth, pointsVectorLength - ghostHalfWidth);
                var randomDelta = randomLengthFromLeftBorder * normalizedPointsVector;

                var randomPoint = firstPoint + randomDelta;
                return randomPoint;
            }

            bool TryUpdateClosestPoint(Vector3 pointToCheck)
            {
                var sqrDistance = (ghostPosition - pointToCheck).sqrMagnitude;
                var isClosest = sqrDistance < closestSafeZoneSqrDistance;
                
                if (isClosest)
                    closestSafeZoneSqrDistance = sqrDistance;
                
                return isClosest;
            }
        }
    }
}