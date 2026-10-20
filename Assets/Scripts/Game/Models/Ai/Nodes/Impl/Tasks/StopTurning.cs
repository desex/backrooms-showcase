using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Movement;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StopTurning : AEntityAction
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            entity.ReplaceHorizontalMoveDirection(EHorizontalMoveDirection.None);

            if (entity.HasGhostTurnDistance)
                entity.RemoveGhostTurnDistance();
            
            return TaskStatus.Success;
        }
    }
}