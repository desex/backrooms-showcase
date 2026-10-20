using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class CanTurn : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            var canTurn = entity.HasTurnChunkData && entity.IsReadyToTurn;
            
            return canTurn ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}