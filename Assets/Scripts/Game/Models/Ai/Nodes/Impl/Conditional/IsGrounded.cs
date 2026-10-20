using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsGrounded : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            return entity.IsGrounded ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}