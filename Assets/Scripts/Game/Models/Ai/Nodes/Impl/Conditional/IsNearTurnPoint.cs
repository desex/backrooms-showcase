using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsNearTurnPoint : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            if (!entity.HasGhostTurnDistance || !entity.HasCurrentChunkId)
                return TaskStatus.Failure;
            
            var currentChunkUid = entity.CurrentChunkId.Value;
            var currentChunk = Game.GetEntityWithUid(currentChunkUid);

            if (currentChunk is not { HasChunkAnchors: true })
                return TaskStatus.Failure;

            var chunkAnchors = currentChunk.ChunkAnchors.Value;
            
            var startAnchor = chunkAnchors.StartAnchor;
            var forwardDirection = startAnchor.forward;

            var ghostPosition = entity.Position.Value;
            
            var startAnchorPosition = startAnchor.position;
            var ghostLocalChunkPosition = ghostPosition - startAnchorPosition;
            var sqrDistanceFromChunkStartBorder = Vector3.Project(ghostLocalChunkPosition, forwardDirection).sqrMagnitude;

            var ghostTurnDistance = entity.GhostTurnDistance.Value;
            var isNeedDistancePassed = sqrDistanceFromChunkStartBorder >= ghostTurnDistance * ghostTurnDistance;

            return isNeedDistancePassed ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}