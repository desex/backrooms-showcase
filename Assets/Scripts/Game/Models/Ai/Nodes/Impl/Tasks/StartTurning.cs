using System;
using BehaviorDesigner.Runtime.Tasks;
using Game.Utils.Movement;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class StartTurning : AEntityAction
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

            var turnData = entity.TurnChunkData.Value;
            var turnDirection = turnData.Direction;

            EHorizontalMoveDirection horizontalMoveDirection;

            switch (turnDirection)
            {
                case ETurnDirection.Left:
                    horizontalMoveDirection = EHorizontalMoveDirection.Left;
                    break;
                case ETurnDirection.Right:
                    horizontalMoveDirection = EHorizontalMoveDirection.Right;
                    break;
                case ETurnDirection.Both:
                    var turnRight = _randomProvider.Range(0f, 1f) > 0.5f;
                    horizontalMoveDirection =
                        turnRight ? EHorizontalMoveDirection.Right : EHorizontalMoveDirection.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnDirection), turnDirection, null);
            }

            entity.ReplaceHorizontalMoveDirection(horizontalMoveDirection);

            _commandBuffer.StartPlayerTurn(SelfUid);

            return TaskStatus.Success;
        }
    }
}