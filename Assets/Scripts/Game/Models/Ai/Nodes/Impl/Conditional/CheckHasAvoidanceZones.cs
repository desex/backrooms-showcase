using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Models.Ai.Nodes.Impl.Conditional
{
    public class CheckHasAvoidanceZones : AEntityConditional
    {
        [SerializeField] private bool _hasAvoidanceZones;

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            
            
            return entity.HasGhostAvoidanceZones == _hasAvoidanceZones && entity.GhostAvoidanceZones.Value != null
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}