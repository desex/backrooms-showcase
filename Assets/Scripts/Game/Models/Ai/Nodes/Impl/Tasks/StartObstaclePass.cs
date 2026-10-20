using BehaviorDesigner.Runtime.Tasks;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StartObstaclePass : AEntityAction
    {
        private ICommandBuffer _commandBuffer;
        private IRandomProvider _randomProvider;

        [Inject] 
        private void Construct(
            ICommandBuffer commandBuffer,
            IRandomProvider randomProvider
        )
        {
            _commandBuffer = commandBuffer;
            _randomProvider = randomProvider;
        }

        public override TaskStatus OnUpdate()
        {
            var entity = GetEntity();
            if (!entity.HasPassObstacleMovePoints)
                return TaskStatus.Failure;
            
            var passObstacleData = entity.PassObstacleMovePoints.Value;
            var movePoints = passObstacleData.MovePoints;
            var chosenType = _randomProvider.Range(0, movePoints.Length); 
            var chosenPassObstacleType = movePoints[chosenType].PassObstacleType;
            
            _commandBuffer.TryDashWhenInObstacle(entity.Uid.Value, chosenPassObstacleType);
            return TaskStatus.Success;
        }
    }
}