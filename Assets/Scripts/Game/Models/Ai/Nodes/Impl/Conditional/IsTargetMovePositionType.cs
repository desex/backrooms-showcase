using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Ghost;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsTargetMovePositionType : AEntityConditional
    {
        [SerializeField] private EGhostMovePositionType _typeToCheck;
        
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            if (!entity.HasGhostRouteData)
                return TaskStatus.Failure;

            var positionData = entity.GhostRouteData.Value;
            
            return positionData.Type == _typeToCheck
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}