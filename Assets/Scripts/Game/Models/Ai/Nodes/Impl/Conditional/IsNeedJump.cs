using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsNeedJump : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            return entity.HasGhostNeedJumpType ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}