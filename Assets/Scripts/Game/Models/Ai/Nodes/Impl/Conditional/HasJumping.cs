using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class HasJumping : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            return entity.HasJumping ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}