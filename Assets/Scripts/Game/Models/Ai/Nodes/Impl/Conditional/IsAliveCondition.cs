using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsAliveCondition : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            return entity.IsDead ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}