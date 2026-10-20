using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsTargetMovePositionReached : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            if (!entity.HasGhostRouteData)
                return TaskStatus.Failure;
            
            return !entity.HasGhostCurrentRouteStatus ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}