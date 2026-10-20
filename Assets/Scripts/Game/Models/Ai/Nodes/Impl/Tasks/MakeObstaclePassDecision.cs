using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Ghost;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class MakeObstaclePassDecision : AEntityAction
    {
        private IRandomProvider _randomProvider;

        [Inject]
        private void Construct(IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider;
        }

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();

            var randomValue = _randomProvider.Value;
            ref var chances = ref entity.GhostChances.Value;

            var result = randomValue < chances.ObstacleFastZoneChance
                ? EGhostZoneMovementResult.Fast
                : EGhostZoneMovementResult.Slow;

            entity.ReplaceGhostMovementWithZonesResult(result);
            return TaskStatus.Success;
        }
    }
}