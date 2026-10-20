using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class CalculateTurnDistance : AEntityAction
    {
        private IGhostsParameters _ghostsParameters;
        private IRandomProvider _randomProvider;

        [Inject]
        private void Construct(
            IGhostsParameters ghostsParameters, 
            IRandomProvider randomProvider
        )
        {
            _ghostsParameters = ghostsParameters;
            _randomProvider = randomProvider;
        }

        public override TaskStatus OnUpdate()
        {
            var ghost = GetEntity();

            var turnDistanceRange = _ghostsParameters.TurnDistanceRange;
            var randomTurnDistance = _randomProvider.Range(turnDistanceRange.x, turnDistanceRange.y);

            ghost.ReplaceGhostTurnDistance(randomTurnDistance);

            return TaskStatus.Success;
        }
    }
}