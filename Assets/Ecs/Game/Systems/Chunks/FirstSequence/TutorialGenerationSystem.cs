using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
using Ecs.Utils;
using Ecs.Utils.Chunks;
using Ecs.Utils.Groups;
using Game.Services.Analytics;
using Game.Services.DistanceCalculation;
using Game.Services.Generation;
using Game.Services.Tutorial;
using Game.Utils.Chunks;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Chunks.FirstSequence
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 30, nameof(EFeatures.Initialization))]
    public class TutorialSequenceGenerationSystem : IInitializeSystem
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IChunkGenerationServiceProvider _chunkGenerationServiceProvider;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IGenerationTasksHandler _generationTasksHandler;
        private readonly ITutorialParameters _tutorialParameters;
        private readonly ITutorialSequencesProvider _tutorialSequencesProvider;

        public TutorialSequenceGenerationSystem(
            ITutorialParameters tutorialParameters,
            IAnalyticsService analyticsService,
            IChunkSegmentService chunkSegmentService,
            GameContext game,
            ITutorialSequencesProvider tutorialSequencesProvider,
            IGameGroupUtils gameGroupUtils,
            IGenerationTasksHandler generationTasksHandler,
            IChunkGenerationServiceProvider chunkGenerationServiceProvider
        )
        {
            _tutorialParameters = tutorialParameters;
            _analyticsService = analyticsService;
            _chunkSegmentService = chunkSegmentService;
            _game = game;
            _tutorialSequencesProvider = tutorialSequencesProvider;
            _gameGroupUtils = gameGroupUtils;
            _generationTasksHandler = generationTasksHandler;
            _chunkGenerationServiceProvider = chunkGenerationServiceProvider;
        }

        public void Initialize()
        {
            _generationTasksHandler.AddTaskToQueue(InitializeAsync);
        }

        private async UniTask InitializeAsync(CancellationToken token)
        {
            var tutorial = _game.TutorialEntity;
            var isInTutorial = tutorial.IsInTutorial;
            if(!isInTutorial)
                return;

            _tutorialSequencesProvider.TryGetNextTutorialSequence(out var firstSequenceData);
            var sequentialParameters = firstSequenceData.Parameters;
            var initialChunkOffset = _tutorialParameters.InitialChunkOffset;
            _chunkSegmentService.SetStartOffset(initialChunkOffset.magnitude);

            _chunkGenerationServiceProvider.TryChangeService(true);
            var chunkGenerationService = _chunkGenerationServiceProvider.ChunkGenerationService;

            await _chunkGenerationServiceProvider.ChunkGenerationService.GenerateFirstSequence(
                sequentialParameters,
                initialChunkOffset,
                token
            );

            var generatedSequenceUid = chunkGenerationService.LastChunkUid;
            var generatedSequence = _game.GetEntityWithUid(generatedSequenceUid);

            generatedSequence.ReplaceTutorialChunkData(firstSequenceData.ChunkData);

            _analyticsService.SetCurrentChunk(generatedSequenceUid);

            using var _ = _gameGroupUtils.GetCharacters(out var characters);

            foreach (var entity in characters)
                entity.ReplaceCurrentChunkId(generatedSequenceUid);

            var chunkNode = generatedSequence.ChunkNode.Value;

            await GenerateTutorialNextChunks(chunkNode, token);

            _game.PlayerEntity.IsUseGravity = true;
        }

        private async UniTask GenerateTutorialNextChunks(ChunkNode firstSequenceNode, CancellationToken token)
        {
            var node = firstSequenceNode;

            for (var i = 0; i < _tutorialParameters.FirstSequencesCount; i++)
            {
                await _chunkGenerationServiceProvider.ChunkGenerationService.GenerateNextChunk(node, token);

                node = _game.GetEntityWithUid(_chunkGenerationServiceProvider.ChunkGenerationService.LastChunkUid)
                    .ChunkNode.Value;
            }
        }
    }
}