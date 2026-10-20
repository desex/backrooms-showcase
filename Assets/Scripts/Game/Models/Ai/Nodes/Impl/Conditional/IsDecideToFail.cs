using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Ghost;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsDecideToFail : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            var movementResult = entity.GhostChancedActionResult.Value;
            return movementResult == EGhostSelectedActionResult.Fail ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}