using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class HasPlankRun : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            return entity.HasPlankRun ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}