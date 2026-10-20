using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using Game.Services.Player;
using Game.Utils.Ghost;
using Game.Utils.Movement;
using UnityEngine;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class MoveToTargetPosition : AEntityAction
    {
        [SerializeField] private float _sqrThreshold = 1f;
        
        private IPlayerSpeedService _playerSpeedService;
        private IGhostsParameters _ghostsParameters;

        [Inject]
        private void Construct(
            IPlayerSpeedService playerSpeedService,
            IGhostsParameters ghostsParameters
        )
        {
            _playerSpeedService = playerSpeedService;
            _ghostsParameters = ghostsParameters;
        }

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            if (!entity.HasGhostRouteData)
                return TaskStatus.Failure;

            if (!TryUpdateCurrentMoveVector(entity, out var sideProjectedMoveVector))
                return TaskStatus.Failure;
            
            var rightDirection = entity.Transform.Value.right;

            var sqrSideMoveDistance = sideProjectedMoveVector.sqrMagnitude;
            var sqrThreshold = _ghostsParameters.StopTiltingThreshold * _ghostsParameters.StopTiltingThreshold;
            
            var currentRouteStatus = entity.GhostCurrentRouteStatus.Value;
            var waypointHorizontalMoveDirection = currentRouteStatus.WaypointHorizontalMoveDirection;
            var canBlockMovingInOppositeDirection = !entity.HasTargetRotation;
            var isPointInOppositeDirection = canBlockMovingInOppositeDirection
                                          && IsNeedMoveInOppositeDirection(
                                                 sideProjectedMoveVector,
                                                 waypointHorizontalMoveDirection
                                             );

            if (sqrSideMoveDistance < sqrThreshold || isPointInOppositeDirection)
            {
                entity.ReplaceHorizontalMoveDirection(EHorizontalMoveDirection.None);
                entity.ReplaceHorizontalSpeed(0f);
                return TaskStatus.Success;
            }

            var needMoveRight = Vector3.Dot(sideProjectedMoveVector, rightDirection) > 0;
            var needMoveDirection = needMoveRight ? EHorizontalMoveDirection.Right : EHorizontalMoveDirection.Left;

            var tiltingDirection = needMoveRight ? 1 : -1;

            var routeData = entity.GhostRouteData.Value;
            var currentWaypointIndex = currentRouteStatus.CurrentWaypointIndex;
            var previousWaypointPosition = currentWaypointIndex == 0
                ? routeData.RouteStartPosition
                : routeData.MovePoints[currentWaypointIndex - 1];
            var ghostPosition = entity.Position.Value;
            var passedSqrDistanceToCurrentWaypoint = (ghostPosition - previousWaypointPosition).sqrMagnitude; 
            var summedPassedDistance = passedSqrDistanceToCurrentWaypoint + currentRouteStatus.SqrDistanceFromPassedPoints;
            var passedDistanceSqrRatio = summedPassedDistance / routeData.FullRouteSqrDistance;
            var speedRatio = routeData.VelocityRatioToPassedDistance.Evaluate(passedDistanceSqrRatio);

            var horizontalSpeed = _playerSpeedService.GetHorizontalSpeed(entity) * tiltingDirection * speedRatio;

            entity.ReplaceHorizontalMoveDirection(needMoveDirection);
            entity.ReplaceHorizontalSpeed(horizontalSpeed);
            
            return TaskStatus.Success;
        }

        private bool TryUpdateCurrentMoveVector(GameEntity entity, out Vector3 horizontalNeedMoveVector)
        {
            horizontalNeedMoveVector = Vector3.zero;
            
            if (!entity.HasGhostCurrentRouteStatus)
                return false;
            
            var routeData = entity.GhostRouteData.Value;
            var currentRouteStatus = entity.GhostCurrentRouteStatus.Value;

            var movePoints = routeData.MovePoints;

            var ghostPosition = entity.Position.Value;
            var ghostTransform = entity.Transform.Value;
            var forwardDirection = ghostTransform.forward;
            var rightDirection = ghostTransform.right;
            
            var currentWaypointIndex = currentRouteStatus.CurrentWaypointIndex;
            var passedPointsSqrDistance = currentRouteStatus.SqrDistanceFromPassedPoints;
            
            for (var i = currentWaypointIndex; i < movePoints.Count; i++)
            {
                var isNotCurrentWaypoint = i != currentWaypointIndex;
                var targetMovePosition = movePoints[i];
                var previousPosition = isNotCurrentWaypoint ? movePoints[i - 1] : ghostPosition;
                var moveVector = targetMovePosition - previousPosition;
                horizontalNeedMoveVector = Vector3.Project(moveVector, rightDirection);
                
                var isPointReached = IsPointReached(moveVector, forwardDirection);

                if (isPointReached)
                {
                    var passedVector = isNotCurrentWaypoint
                        ? moveVector
                        : targetMovePosition - routeData.RouteStartPosition; 
                    passedPointsSqrDistance += passedVector.sqrMagnitude;
                    continue;
                }
                
                if (isNotCurrentWaypoint)
                {
                    var updatedData = new GhostCurrentRouteStatusData(passedPointsSqrDistance, i, horizontalNeedMoveVector);
                    entity.ReplaceGhostCurrentRouteStatus(updatedData);
                }
                
                return true;
            }
            
            entity.RemoveGhostCurrentRouteStatus();
            return false;
        }
        
        private bool IsPointReached(Vector3 moveVector, Vector3 forwardDirection)
        {
            if (moveVector.sqrMagnitude <= _sqrThreshold)
                return true;

            var dot = Vector3.Dot(moveVector, forwardDirection);
            var isMovePointBehind = dot < 0;
            return isMovePointBehind;
        }

        private static bool IsNeedMoveInOppositeDirection(Vector3 moveVector, Vector3 lastMoveVector)
        {
            var dot = Vector3.Dot(moveVector, lastMoveVector);
            var isMovePointInOppositeDirection = dot < 0;
            return isMovePointInOppositeDirection;
        }
    }
}