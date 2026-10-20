using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsTurning : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            return entity.HasTurning ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}