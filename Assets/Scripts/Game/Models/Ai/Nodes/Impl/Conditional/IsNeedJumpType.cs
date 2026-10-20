using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Ghost;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class IsNeedJumpType : AEntityConditional
    {
        [SerializeField] private EGhostSimpleJumpType _needSimpleJumpType;
        
        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            if (!entity.HasGhostNeedJumpType)
                return TaskStatus.Failure;

            var jumpType = entity.GhostNeedJumpType.Value;
            
            return jumpType == _needSimpleJumpType ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}