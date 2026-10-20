using Ecs.Utils;
using Game.Services.DistanceCalculation;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 910, nameof(EFeatures.Move))]
    public sealed class DistancePassUpdateSystem : APausableGameStateConditionUpdateSystem
    {
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;

        public DistancePassUpdateSystem(
            GameContext game,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService
        ) : base(game)
        {
            _distanceCalculationService = distanceCalculationService;
            _chunkSegmentService = chunkSegmentService;
        }

        protected override bool GameStateCondition(EGameState gameState)
        {
            return gameState == EGameState.Game;
        }

        protected override void UpdateSystem()
        {
            var player = Game.PlayerEntity;

            if (player.IsDead)
                return;

            if (!player.HasCharacterController)
                return;

            if (player.IsInterruptPlayerMovement)
                return;

            var distance = _distanceCalculationService.GetPlayerDistance(_chunkSegmentService.Segments);

            player.ReplacePassedDistance(distance);
        }
    }
}