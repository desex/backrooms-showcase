using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Movement;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StopMovingToTargetPosition : AEntityAction
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            if (entity.HasGhostRouteData)
                entity.RemoveGhostRouteData();
            
            if (entity.HasGhostCurrentRouteStatus)
                entity.RemoveGhostCurrentRouteStatus();
            
            entity.ReplaceHorizontalMoveDirection(EHorizontalMoveDirection.None);
            entity.ReplaceHorizontalSpeed(0f);
                
            return TaskStatus.Success;
        }
    }
}