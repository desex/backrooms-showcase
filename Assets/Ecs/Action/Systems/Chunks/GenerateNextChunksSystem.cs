using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
using Ecs.Action.Commands.Chunks;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.Chunks;
using Game.Services.DistanceCalculation;
using Game.Services.Generation;
using Game.Services.Providers;
using Game.Services.Tutorial;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 0, nameof(EFeatures.Chunk))]
    public sealed class GenerateNextChunksSystem : ForEachCommandUpdateSystem<GenerateNextChunksCommand>
    {
        private const float DISTANCE_THRESHOLD = 1f;
        
        private readonly IChunkGenerationServiceProvider _chunkGenerationServiceProvider;
        private readonly ITutorialSequencesProvider _tutorialSequencesProvider;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly ITutorialParameters _tutorialParameters;
        private readonly GameContext _game;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public GenerateNextChunksSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IGenerationTasksHandler generationTasksHandler,
            IChunkGenerationServiceProvider chunkGenerationServiceProvider,
            ITutorialSequencesProvider tutorialSequencesProvider,
            IChunkSegmentService chunkSegmentService,
            ITutorialParameters tutorialParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _generationResourcesProvider = generationResourcesProvider;
            _distanceCalculationService = distanceCalculationService;
            _generationTasksHandler = generationTasksHandler;
            _chunkGenerationServiceProvider = chunkGenerationServiceProvider;
            _tutorialSequencesProvider = tutorialSequencesProvider;
            _chunkSegmentService = chunkSegmentService;
            _tutorialParameters = tutorialParameters;
        }

        protected override void Execute(ref GenerateNextChunksCommand command)
        {
            var chunkId = command.CurrentEnteredChunkId;

            _generationTasksHandler.AddTaskToQueue(async token => await GenerateNextChunkAsync(chunkId, token));
        }

        private async UniTask GenerateNextChunkAsync(Uid chunkId, CancellationToken token)
        {
            var chunk = _game.GetEntityWithUid(chunkId);

            if (chunk.IsVisited)
                return;

            chunk.IsVisited = true;

            var chunkGenerationSettings = _generationResourcesProvider.ChunkGenerationSettings;
            var useGeneration = chunkGenerationSettings.UseGeneration;
            var minChunkLength = chunkGenerationSettings.ChunkSequenceLength;

            if (!useGeneration)
                return;

            var entityId = chunkId;
            var entity = _game.GetEntityWithUid(entityId);

            var node = entity.ChunkNode.Value;

            var playerLastDistance = CountDistance();

            await GenerateChunks(playerLastDistance, minChunkLength, node, token);
        }

        private async UniTask GenerateChunks(
            float playerLastDistance,
            int minChunkLength,
            ChunkNode node,
            CancellationToken token
        )
        {
            if (_game.TutorialEntity.IsInTutorial)
            {
                var wasTutorialChunkGenerated = await TryGenerateTutorialChunks(node, token);
                if (wasTutorialChunkGenerated)
                    return;
            }
            
            while (playerLastDistance < minChunkLength)
            {
                _chunkGenerationServiceProvider.TryChangeService();

                await node.MakeActionWithEveryEndNode(
                    _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk,
                    token
                );

                AddChunkLength(ref playerLastDistance);
            }
        }

        private async UniTask<bool> TryGenerateTutorialChunks(ChunkNode node, CancellationToken token)
        {
            var sequencesQueueCount = _tutorialSequencesProvider.SequencesQueue.Count;

            if (sequencesQueueCount <= 0)
                return false;
            
            await node.MakeActionWithEveryEndNode(
                _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk,
                token
            );

            var wasLastSequenceSpawned = _tutorialSequencesProvider.SequencesQueue.Count == 0;

            if (!wasLastSequenceSpawned)
                return true;

            var lastTutorialChunkDistance = _chunkSegmentService.LastChunkSegment.EndDistance;
            CheckIfLastTutorialChunkEndsInZeroDistance(lastTutorialChunkDistance);

            return true;
        }

        private void CheckIfLastTutorialChunkEndsInZeroDistance(float lastTutorialChunkDistance)
        {
            if (Mathf.Abs(lastTutorialChunkDistance) < DISTANCE_THRESHOLD)
                return;

            var correctTutorialDistance = _tutorialParameters.TutorialDistanceInUnits + lastTutorialChunkDistance;
            correctTutorialDistance -= _tutorialParameters.FirstTutorialDistanceThresholdInUnits;
            
            Debug.LogError($"{nameof(GenerateNextChunksSystem)} | TutorialDistanceInUnits is incorrect! Set it in SO to: {correctTutorialDistance}");
        }

        private void AddChunkLength(ref float playerLastDistance)
        {
            var chunkSegment = _chunkSegmentService.LastChunkSegment;

            if (chunkSegment.Segment == ESegment.None)
            {
                var chunkDistance = chunkSegment.EndDistance - chunkSegment.StartDistance;
                playerLastDistance += chunkDistance;

                return;
            }

            var segmentsList = _chunkSegmentService.Segments;

            var indexStartChunk = 0;

            for (var i = segmentsList.Count - 1; i >= 0; i--)
            {
                if (segmentsList[i].Segment == ESegment.Start)
                    indexStartChunk = i;
            }

            for (var j = indexStartChunk; j < segmentsList.Count; j++)
            {
                var chunkDistance = segmentsList[j].EndDistance - segmentsList[j].StartDistance;
                playerLastDistance += chunkDistance;
            }
        }

        private float CountDistance()
        {
            var playerDistance = _distanceCalculationService.GetPlayerDistance(_chunkSegmentService.Segments);
            var lastSegment = _chunkSegmentService.LastChunkSegment;

            return lastSegment.EndDistance - playerDistance;
        }
    }
}