using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class SetIsInAction : AEntityAction
    {
        [SerializeField] private bool _isInAction;

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            entity.IsInAction = _isInAction;
            return TaskStatus.Success;
        }
    }
}