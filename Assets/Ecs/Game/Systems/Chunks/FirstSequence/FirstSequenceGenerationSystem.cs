using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Utils;
using Ecs.Utils.Chunks;
using Ecs.Utils.Groups;
using Game.Services.Analytics;
using Game.Services.DistanceCalculation;
using Game.Services.Generation;
using Game.Services.Providers;
using Game.Utils.Chunks;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Chunks.FirstSequence
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 30, nameof(EFeatures.Initialization))]
    public class FirstSequenceGenerationSystem : IInitializeSystem
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IChunkGenerationServiceProvider _chunkGenerationServiceProvider;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IGenerationTasksHandler _generationTasksHandler;

        public FirstSequenceGenerationSystem(
            IAnalyticsService analyticsService,
            IGenerationResourcesProvider generationResourcesProvider,
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            IGenerationTasksHandler generationTasksHandler,
            IChunkGenerationServiceProvider chunkGenerationServiceProvider,
            IChunkSegmentService chunkSegmentService
        )
        {
            _analyticsService = analyticsService;
            _generationResourcesProvider = generationResourcesProvider;
            _game = game;
            _gameGroupUtils = gameGroupUtils;
            _generationTasksHandler = generationTasksHandler;
            _chunkGenerationServiceProvider = chunkGenerationServiceProvider;
            _chunkSegmentService = chunkSegmentService;
        }

        public void Initialize()
        {
            _generationTasksHandler.AddTaskToQueue(InitializeAsync);
        }

        private async UniTask InitializeAsync(CancellationToken token)
        {
            var tutorial = _game.TutorialEntity;
            var isInTutorial = tutorial.IsInTutorial;
            if(isInTutorial)
                return;

            var sequenceGenerationSettings = _generationResourcesProvider.SequenceGenerationSettings;
            var sequentialParameters = sequenceGenerationSettings.FirstSequenceParameters;

            var chunkGenerationSettings = _generationResourcesProvider.ChunkGenerationSettings;
            var initialChunkOffset = chunkGenerationSettings.InitialChunkOffset;
            _chunkSegmentService.SetStartOffset(initialChunkOffset.magnitude);

            _chunkGenerationServiceProvider.TryChangeService(true);
            var chunkGenerationService = _chunkGenerationServiceProvider.ChunkGenerationService;

            await chunkGenerationService.GenerateFirstSequence(
                sequentialParameters,
                initialChunkOffset,
                token
            );

            var generatedSequenceUid = chunkGenerationService.LastChunkUid;
            var generatedSequence = _game.GetEntityWithUid(generatedSequenceUid);

            _analyticsService.SetCurrentChunk(generatedSequenceUid);

            using var _ = _gameGroupUtils.GetCharacters(out var characters);

            foreach (var entity in characters)
                entity.ReplaceCurrentChunkId(generatedSequenceUid);

            var chunkNode = generatedSequence.ChunkNode.Value;

            await GenerateNormalNextChunks(chunkNode, token);

            _game.PlayerEntity.IsUseGravity = true;
        }

        private async UniTask GenerateNormalNextChunks(ChunkNode node, CancellationToken token)
        {
            var chunkLength = 0f;
            var chunksToGenerate = _generationResourcesProvider.ChunkGenerationSettings.ChunkSequenceLength;

            while (chunkLength < chunksToGenerate)
            {
                await node.MakeActionWithEveryEndNode(
                    _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk,
                    token
                );

                var chunkSegment = _chunkSegmentService.LastChunkSegment;

                var chunkSegmentLength = chunkSegment.EndDistance - chunkSegment.StartDistance;

                chunkLength += chunkSegmentLength;
            }
        }
    }
}