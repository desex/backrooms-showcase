using System;
using BehaviorDesigner.Runtime.Tasks;
using Db.Ghost;
using Game.Utils.Ghost;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Game.Models.Ai.Nodes.Impl.Tasks
{
    public class MakeChancedActionDecision : AEntityAction
    {
        [SerializeField] private EGhostActionWithChancesType _ghostMovementType;

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
            var entity = GetEntity();

            EGhostSelectedActionResult result;

            if (entity.IsGhostFarAhead)
            {
                result = EGhostSelectedActionResult.Fail;
            }
            else
            {
                var randomValue = _randomProvider.Value;
                ref var allChances = ref entity.GhostChances.Value;

                var failChance = _ghostMovementType switch
                {
                    EGhostActionWithChancesType.Jump => allChances.JumpFailChance,
                    EGhostActionWithChancesType.Comeback => allChances.ComeBackFailChance,
                    EGhostActionWithChancesType.Turn => allChances.TurnFailChance,
                    EGhostActionWithChancesType.HandleObstacle => allChances.ObstacleFailChance,
                    _ => throw new ArgumentOutOfRangeException(nameof(_ghostMovementType),
                                                               _ghostMovementType,
                                                               null
                    ),
                };

                result = randomValue < failChance
                    ? EGhostSelectedActionResult.Fail
                    : EGhostSelectedActionResult.Success;
            }

            switch (result)
            {
                case EGhostSelectedActionResult.Fail:
                    ApplyChancesMovementResultChange(entity, -_ghostsParameters.FailureMovementIncreaseChanceDelta);
                    break;
                case EGhostSelectedActionResult.Success:
                    ApplyChancesMovementResultChange(entity, _ghostsParameters.SuccessMovementDecreaseChanceDelta);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            entity.ReplaceGhostChancedActionResult(result);
            return TaskStatus.Success;
        }

        private static void ApplyChancesMovementResultChange(GameEntity entity, float chanceDelta)
        {
            ref var originalChances = ref entity.GhostChances.Value;
            var newChances = new GhostChancesData(
                jumpFailChance: ApplyChanceDelta(originalChances.JumpFailChance, chanceDelta),
                comeBackFailChance: ApplyChanceDelta(originalChances.ComeBackFailChance, chanceDelta),
                turnFailChance: originalChances.TurnFailChance,
                obstacleFailChance: ApplyChanceDelta(originalChances.ObstacleFailChance, chanceDelta),
                obstacleFastZoneChance: originalChances.ObstacleFastZoneChance
            );

            entity.ReplaceGhostChances(newChances);

            return;

            float ApplyChanceDelta(float baseValue, float delta)
            {
                return Mathf.Clamp(baseValue + delta, 0f, 1f);
            }
        }
    }
}
