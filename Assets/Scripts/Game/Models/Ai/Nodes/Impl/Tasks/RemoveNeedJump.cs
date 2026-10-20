using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class RemoveNeedJump : AEntityAction
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            if (entity.HasGhostNeedJumpType)
                entity.RemoveGhostNeedJumpType();

            return TaskStatus.Success;
        }
    }
}