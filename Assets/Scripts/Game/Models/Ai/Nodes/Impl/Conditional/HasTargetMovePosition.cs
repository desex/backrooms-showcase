using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class HasTargetMovePosition : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            return entity.HasGhostRouteData
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}