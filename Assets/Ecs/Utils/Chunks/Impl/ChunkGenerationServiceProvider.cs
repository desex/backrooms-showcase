using Db.Score;
using Game.Services.Chunks;
using Game.Services.Chunks.Impl;
using Game.Services.DistanceCalculation;
using Game.Services.Providers;
using Game.Services.Tutorial;
using Zenject;

namespace Ecs.Utils.Chunks.Impl
{
    public class ChunkGenerationServiceProvider : IChunkGenerationServiceProvider, IInitializable
    {
        private readonly GameContext _game;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IChunkGenerationService _randomChunkGenerationService;
        private readonly IScoreParameters _scoreParameters;
        private readonly IChunkGenerationService _sequentialChunkGenerationService;
        private readonly IChunkGenerationService _tutorialChunkGenerationService;
        private readonly ITutorialSequencesProvider _tutorialSequencesProvider;
        private readonly IChunkSegmentService _chunkSegmentService;

        public ChunkGenerationServiceProvider(
            SequentialChunkGenerationService sequentialChunkGenerationService,
            TutorialChunkGenerationService tutorialChunkGenerationService,
            IGenerationResourcesProvider generationResourcesProvider,
            ITutorialSequencesProvider tutorialSequencesProvider,
            RandomChunkGenerationService randomChunkGenerationService,
            IScoreParameters scoreParameters,
            GameContext game,
            IChunkSegmentService chunkSegmentService
        )
        {
            _sequentialChunkGenerationService = sequentialChunkGenerationService;
            _tutorialChunkGenerationService = tutorialChunkGenerationService;
            _generationResourcesProvider = generationResourcesProvider;
            _tutorialSequencesProvider = tutorialSequencesProvider;
            _randomChunkGenerationService = randomChunkGenerationService;
            ChunkGenerationService = randomChunkGenerationService;
            _scoreParameters = scoreParameters;
            _game = game;
            _chunkSegmentService = chunkSegmentService;
        }

        public IChunkGenerationService ChunkGenerationService { get; private set; }

        public void TryChangeService(bool isFirstChunk = false)
        {
            var lastTransitionDistance = _game.LevelEntity.LastTransitionDistance.Value;

            var lastSegmentDistance = isFirstChunk ? 0f : _chunkSegmentService.LastChunkSegment.EndDistance;

            var currentDistance = lastSegmentDistance * _scoreParameters.DistanceToMetersRatio - lastTransitionDistance;

            var sequenceDistanceGroups = _generationResourcesProvider.SequenceGenerationSettings.SequenceDistanceGroups;

            if (_game.TutorialEntity.IsInTutorial && _tutorialSequencesProvider.SequencesQueue.Count != 0)
            {
                ChunkGenerationService = _tutorialChunkGenerationService;
                return;
            }

            if (isFirstChunk
             || sequenceDistanceGroups != null && sequenceDistanceGroups.Count != 0 && DistanceInSequenceGroup())
            {
                ChunkGenerationService = _sequentialChunkGenerationService;
                return;
            }

            ChunkGenerationService = _randomChunkGenerationService;

            return;

            bool DistanceInSequenceGroup()
            {
                foreach (var sequenceDistanceGroup in sequenceDistanceGroups)
                {
                    if (currentDistance >= sequenceDistanceGroup.GroupLength.x
                     && currentDistance <= sequenceDistanceGroup.GroupLength.y)
                        return true;
                }

                return false;
            }
        }

        public void Initialize()
        {
            _sequentialChunkGenerationService.Initialize();
            _randomChunkGenerationService.Initialize();
        }
    }
}