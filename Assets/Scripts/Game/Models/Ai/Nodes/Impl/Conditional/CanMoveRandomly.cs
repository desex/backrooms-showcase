using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using Ecs.Managers;
using Game.Utils.Chunks;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class CanMoveRandomly : AEntityConditional
    {
        private IGhostsParameters _ghostsParameters;

        [Inject]
        private void Construct(IGhostsParameters ghostsParameters)
        {
            _ghostsParameters = ghostsParameters;
        }
        
        public override TaskStatus OnUpdate()
        {
            if (SelfUid == Uid.Empty)
                return TaskStatus.Failure;
            
            var entity = GetEntity();

            if (entity.IsDead
             || entity.IsInAction
             || entity.HasGhostAvoidanceZones
             || entity.IsReadyToTurn
             || entity.HasReadyToObstaclePass
             || !entity.HasCurrentChunkId
             || !entity.IsGrounded
             || entity.HasGhostNeedJumpType
             || entity.HasJumping)
                return TaskStatus.Failure;

            var currentChunkId = entity.CurrentChunkId.Value;
            var currentChunk = Game.GetEntityWithUid(currentChunkId);
            if (currentChunk is not {HasChunkNode: true})
                return TaskStatus.Failure;
            
            var currentChunkNode = currentChunk.ChunkNode.Value;
            var currentChunkType = currentChunkNode.ChunkType;
            var restrictedChunkTypes = _ghostsParameters.RandomTiltRestrictedChunks;

            var isMoveRestricted = (currentChunkType & restrictedChunkTypes) != EChunkType.None;

            return isMoveRestricted ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}