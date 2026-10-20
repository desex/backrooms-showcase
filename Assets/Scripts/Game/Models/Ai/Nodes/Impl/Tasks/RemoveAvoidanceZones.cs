using BehaviorDesigner.Runtime.Tasks;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class RemoveAvoidanceZones : AEntityAction
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            if (entity.HasGhostAvoidanceZones)
                entity.RemoveGhostAvoidanceZones();

            return TaskStatus.Success;
        }
    }
}