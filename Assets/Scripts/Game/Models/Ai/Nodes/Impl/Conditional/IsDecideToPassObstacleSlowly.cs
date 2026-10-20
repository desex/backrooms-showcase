using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Ghost;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsDecideToPassObstacleSlowly : AEntityConditional
    {
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            var movementResult = entity.GhostMovementWithZonesResult.Value;
            return movementResult == EGhostZoneMovementResult.Slow ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}