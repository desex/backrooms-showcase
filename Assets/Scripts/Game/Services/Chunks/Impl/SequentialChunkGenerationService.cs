using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.Score;
using Db.TransitionChunk;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.CheckPoints;
using Game.Services.CollectablesItem;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Services.Providers;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public sealed class SequentialChunkGenerationService : ASequentialChunkGenerationService
    {
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IScoreParameters _scoreParameters;
        private readonly ISequentialChunkPredictionService _sequentialChunkPredictionService;
        private readonly ITransitionChunkParameters _transitionChunkParameters;

        private float _targetTransitDistance;

        public SequentialChunkGenerationService(
            ISequentialObstacleGenerationService sequentialObstacleGenerationService,
            ISequentialCollectableSpawnService sequentialCollectableSpawnService,
            ISequentialWallsGenerationService sequentialWallsGenerationService,
            ISequentialChunkPredictionService sequentialChunkPredictionService,
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService,
            ITransitionChunkParameters transitionChunkParameters,
            ICheckpointService checkpointService,
            IScoreParameters scoreParameters,
            IChunkService chunkService,
            GameContext game
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
            _sequentialChunkPredictionService = sequentialChunkPredictionService;
            _transitionChunkParameters = transitionChunkParameters;
            _scoreParameters = scoreParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        public override void Initialize()
        {
            _targetTransitDistance = _transitionChunkParameters.DistanceBetweenChunks;
        }

        protected override async UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform[] anchors,
            CancellationToken token
        )
        {
            await GenerateNextChunk(chunkEntity, anchors[0], token);
            var currentChunkType = chunkEntity.ChunkNode.Value.ChunkType;
            var isTransitionsChunks = currentChunkType is EChunkType.TransitionHole or EChunkType.TransitionTrain or EChunkType.TransitionTruck;

            if (isTransitionsChunks)
            {
                var transitionSequence = _sequentialChunkPredictionService.ChooseTransitionSequence(chunkEntity);
                var transitionChunk = await GenerateChosenSequence(chunkEntity, anchors[1], transitionSequence, token);
                
                var postTransitionSequence = _sequentialChunkPredictionService.ChoosePostTransitionSequence(transitionChunk);
                await GenerateChosenSequence(transitionChunk, transitionChunk.ChunkAnchors.Value.EndOnlyAnchor, postTransitionSequence, token);
            }
            else
            {
                var chosenSequenceParameters = _sequentialChunkPredictionService.ChooseSequenceForSecondAnchor(chunkEntity);
                await GenerateChosenSequence(chunkEntity, anchors[1], chosenSequenceParameters, token);
            }
            
            
        }

        protected override async UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform endAnchor,
            CancellationToken token
        )
        {
            var sequenceParameters = _sequentialChunkPredictionService.ChooseSequence(chunkEntity.ChunkNode.Value);
            await GenerateChosenSequence(chunkEntity, endAnchor, sequenceParameters, token);
        }
        
        private async UniTask<GameEntity> GenerateChosenSequence(
            GameEntity chunkEntity,
            Transform endAnchor,
            SequenceParameters chosenSequence,
            CancellationToken token
        )
        {
            var chunksInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo;
            var shouldSpawnTransit = ShouldSpawnTransitChunk(endAnchor);

            if (shouldSpawnTransit)
                _targetTransitDistance += _transitionChunkParameters.DistanceBetweenChunks;

            var newChunkType = shouldSpawnTransit ? EChunkType.Transition : chosenSequence.ChunkType;
            var newChunkEntity = await ChunkService.CreateChunk(newChunkType, token, endAnchor, chunkEntity);

            LastChunkUid = newChunkEntity.Uid.Value;

            if (shouldSpawnTransit)
            {
                await GenerateTransit(newChunkEntity, token);
            }
            else
            {
                newChunkEntity.ChunkNode.Value.SequenceParameters = chosenSequence;

                await GenerateSequentially(
                    chosenSequence,
                    new GenerateChunkData
                    {
                        ChunkInfo = chunksInfo[newChunkType],
                        NewChunk = newChunkEntity.Link.View as ChunkView,
                        NewChunkEntity = newChunkEntity,
                        NewChunkType = newChunkType,
                    },
                    token
                );
            }
            
            return newChunkEntity;
        }

        private bool ShouldSpawnTransitChunk(Transform endAnchor)
        {
            var chunkSpawnPositionInMeters = DistanceCalculationService.GetDistance(endAnchor.position, _chunkSegmentService.Segments)
                                             * _scoreParameters.DistanceToMetersRatio;

            return chunkSpawnPositionInMeters >= _targetTransitDistance;
        }

        private async UniTask GenerateTransit(GameEntity currentChunkEntity, CancellationToken token)
        {
            var currentChunkNode = currentChunkEntity.ChunkNode.Value;
            var pointNodes = ChunkSegmentService.RegisterChunk(currentChunkEntity);
            DistanceCalculationService.CalculateDistances(ChunkSegmentService.Segments);

            currentChunkNode.IsGenerationStopped = true;

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            foreach (var node in pointNodes)
                CheckpointService.TrySpawn(node.StartDistance, node.EndDistance);
        }
    }
}