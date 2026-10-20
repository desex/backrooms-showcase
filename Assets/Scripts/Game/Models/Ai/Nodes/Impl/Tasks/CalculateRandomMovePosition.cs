using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using Game.Utils.Ghost;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class CalculateRandomMovePosition : AEntityAction
    {
        private IGhostsParameters _ghostsParameters;
        private IRandomProvider _randomProvider;

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

            if (!ghost.HasCurrentChunkId)
                return TaskStatus.Failure;

            var currentChunkUid = ghost.CurrentChunkId.Value;
            var currentChunk = Game.GetEntityWithUid(currentChunkUid);

            if (!currentChunk.HasChunkAnchors)
                return TaskStatus.Failure;

            var (missPoint, finalMovePoint) = GenerateWaypoints(ghost, currentChunk);
            
            var movePoints = new List<Vector3>
            {
                missPoint,
                finalMovePoint,
            };

            var ghostPosition = ghost.Position.Value;

            var totalSqrDistance = (missPoint - ghostPosition).sqrMagnitude;
            totalSqrDistance += (finalMovePoint - missPoint).sqrMagnitude;
            
            var randomTiltCurve = _ghostsParameters.RandomTiltVelocityRatioToPassedDistanceCurve;
            
            var moveData = new GhostRouteData(
                movePoints,
                EGhostMovePositionType.AvoidObstacle,
                randomTiltCurve,
                totalSqrDistance,
                ghostPosition
            );
            
            var firstMoveVector = missPoint - ghostPosition;
            var ghostRightDirection = ghost.Transform.Value.right;
            var horizontalMoveDirection = Vector3.Project(firstMoveVector, ghostRightDirection);
            ghost.ReplaceGhostRouteData(moveData);
            ghost.ReplaceGhostCurrentRouteStatus(new(0f, 0, horizontalMoveDirection));
            
            return TaskStatus.Success;
        }
        
        private (Vector3 missPoint, Vector3 finalMovePoint) GenerateWaypoints(GameEntity ghost, GameEntity currentChunk)
        {
            var sidesDeltaRange = _ghostsParameters.RandomTiltPointSidesDeltaRange;
            var forwardDeltaRange = _ghostsParameters.RandomTiltPointForwardDeltaRange;
            var missSidesDeltaRange = _ghostsParameters.RandomTiltMissPointSidesDeltaRange;
            var missForwardDeltaRange = _ghostsParameters.RandomTiltMissPointForwardDeltaRange;
            
            var chunkAnchors = currentChunk.ChunkAnchors.Value;
            var startAnchor = chunkAnchors.StartAnchor;
            var startAnchorPosition = startAnchor.position;
            var chunkForward = startAnchor.forward;
            var chunkRight = startAnchor.right;

            var ghostPosition = ghost.Position.Value;
            var ghostLocalChunkPosition = ghostPosition - startAnchorPosition;
            var ghostChunkCenteredPosition = Vector3.Project(ghostLocalChunkPosition, chunkForward) + startAnchorPosition;

            var sideVectorSign = _randomProvider.Value > 0.5 ? 1 : -1;
            
            var targetDeltaRight = _randomProvider.Range(sidesDeltaRange.x, sidesDeltaRange.y) * sideVectorSign;
            var missDeltaRight = targetDeltaRight - _randomProvider.Range(missSidesDeltaRange.x, missSidesDeltaRange.y) * sideVectorSign;

            var targetDeltaForward = _randomProvider.Range(forwardDeltaRange.x, forwardDeltaRange.y);
            var missDeltaForward = targetDeltaForward - _randomProvider.Range(missForwardDeltaRange.x, missForwardDeltaRange.y);
            
            var missPosition = ghostChunkCenteredPosition + missDeltaRight * chunkRight + missDeltaForward * chunkForward;
            var targetPosition = ghostChunkCenteredPosition + targetDeltaRight * chunkRight + targetDeltaForward * chunkForward;
            return (missPosition, targetPosition);
        }
    }
}