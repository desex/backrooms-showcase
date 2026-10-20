using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Action.Commands.Collectables;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.CollectablesItem;
using Game.Services.DistanceCalculation;
using Game.Services.Generation;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Collectables
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 400, nameof(EFeatures.CollectableItem))]
    public class CollectablesDespawnSystem : ForEachCommandUpdateSystem<CollectablesDespawnCommand>
    {
        private readonly ICollectableSpawnService _collectableSpawnService;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public CollectablesDespawnSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            IGameGroupUtils gameGroupUtils,
            IDistanceCalculationService distanceCalculationService,
            ICollectableSpawnService collectableSpawnService,
            IGenerationTasksHandler generationTasksHandler,
            IChunkSegmentService chunkSegmentService
        ) : base(commandBuffer)
        {
            _game = game;
            _gameGroupUtils = gameGroupUtils;
            _distanceCalculationService = distanceCalculationService;
            _collectableSpawnService = collectableSpawnService;
            _generationTasksHandler = generationTasksHandler;
            _chunkSegmentService = chunkSegmentService;
        }

        protected override void Execute(ref CollectablesDespawnCommand command)
        {
            _generationTasksHandler.AddTaskToQueue(DespawnCollectablesAsync);
        }

        private async UniTask DespawnCollectablesAsync(CancellationToken token)
        {
            if (_game.TutorialEntity.IsInTutorial)
                return;

            using var _ = _gameGroupUtils.GetCollectables(
                out var collectables,
                entity => !entity.HasOwner && entity.HasCollectableItem && entity.IsActive
            );

            foreach (var collectable in collectables)
            {
                var collectablePosition = collectable.Position.Value;
                var playerDistanceInUnit = _distanceCalculationService.GetPlayerDistance(_chunkSegmentService.Segments);
                var collectableDistanceInUnit = _distanceCalculationService.GetDistance(collectablePosition, _chunkSegmentService.Segments);

                if (playerDistanceInUnit < collectableDistanceInUnit)
                    continue;

                await _collectableSpawnService.DespawnCollectable(collectable.Uid.Value, token);
            }
        }
    }
}