using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class CheckReadyForObstaclePass : AEntityConditional
    {
        [SerializeField] private bool _isReadyForObstaclePass;

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            var readyForObstaclePass = entity.HasReadyToObstaclePass && entity.HasPassObstacleMovePoints;
            
            return readyForObstaclePass == _isReadyForObstaclePass ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}