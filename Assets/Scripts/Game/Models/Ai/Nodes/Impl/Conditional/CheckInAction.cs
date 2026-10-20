using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class CheckInAction : AEntityConditional
    {
        [SerializeField] private bool _isInAction;

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            return entity.IsInAction == _isInAction ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}