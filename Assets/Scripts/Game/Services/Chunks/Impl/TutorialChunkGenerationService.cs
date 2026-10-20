using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.Tutorial;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.CheckPoints;
using Game.Services.CollectablesItem;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Services.Providers;
using Game.Services.Tutorial;
using Game.Utils.DistanceCalculation;
using Game.Utils.Tutorial;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public class TutorialChunkGenerationService : ASequentialChunkGenerationService
    {
        private readonly ITutorialSequencesProvider _tutorialSequencesProvider;
        private readonly ITutorialParameters _tutorialParameters;

        public TutorialChunkGenerationService(
            ISequentialObstacleGenerationService sequentialObstacleGenerationService,
            ISequentialCollectableSpawnService sequentialCollectableSpawnService,
            ISequentialWallsGenerationService sequentialWallsGenerationService,
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService,
            ITutorialSequencesProvider tutorialSequencesProvider,
            ICheckpointService checkpointService,
            IChunkService chunkService,
            GameContext game,
            ITutorialParameters tutorialParameters
        ) : base(
            sequentialObstacleGenerationService,
            sequentialCollectableSpawnService,
            sequentialWallsGenerationService,
            generationResourcesProvider,
            distanceCalculationService,
            chunkSegmentService,
            checkpointService,
            chunkService,
            game
        )
        {
            _tutorialSequencesProvider = tutorialSequencesProvider;
            _tutorialParameters = tutorialParameters;
        }

        protected override async UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform endAnchor,
            CancellationToken token
        )
        {
            var chunksInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo;

            var targetSequence = SelectTargetSequence(out var tutorialChunkData);

            var newChunkType = targetSequence.ChunkType;
            var newChunkEntity = await ChunkService.CreateChunk(newChunkType, token, endAnchor, chunkEntity);

            if (tutorialChunkData.HasValue)
                newChunkEntity.ReplaceTutorialChunkData(tutorialChunkData.Value);

            LastChunkUid = newChunkEntity.Uid.Value;

            var generateChunkData = new GenerateChunkData
            {
                ChunkInfo = chunksInfo[newChunkType],
                NewChunk = newChunkEntity.Link.View as ChunkView,
                NewChunkEntity = newChunkEntity,
                NewChunkType = newChunkType,
            };

            await GenerateSequentially(targetSequence, generateChunkData, token);

            return;

            SequenceParameters SelectTargetSequence(out TutorialChunkData? tutorialSequenceChunkData)
            {
                _tutorialSequencesProvider.TryGetNextTutorialSequence(out var sequenceData);

                tutorialSequenceChunkData = sequenceData.ChunkData;
                return sequenceData.Parameters;
            }
        }

        public override async UniTask GenerateFirstSequence(SequenceParameters parameters, Vector3 offset, CancellationToken token)
        {
            await base.GenerateFirstSequence(parameters, offset, token);

            var tutorialDistanceOffset = _tutorialParameters.TutorialDistanceInUnits
                                       - _tutorialParameters.FirstTutorialDistanceThresholdInUnits;

            ChunkSegmentService.ApplyDistanceOffset(tutorialDistanceOffset);
        }
    }
}