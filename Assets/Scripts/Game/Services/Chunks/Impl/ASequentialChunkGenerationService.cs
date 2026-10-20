using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.CheckPoints;
using Game.Services.CollectablesItem;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Services.Providers;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public abstract class ASequentialChunkGenerationService : AChunkGenerationService
    {
        protected readonly ISequentialCollectableSpawnService SequentialCollectableSpawnService;
        protected readonly ISequentialObstacleGenerationService SequentialObstacleGenerationService;
        protected readonly ISequentialWallsGenerationService SequentialWallsGenerationService;

        protected ASequentialChunkGenerationService(
            ISequentialObstacleGenerationService sequentialObstacleGenerationService,
            ISequentialCollectableSpawnService sequentialCollectableSpawnService,
            ISequentialWallsGenerationService sequentialWallsGenerationService,
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService,
            ICheckpointService checkpointService,
            IChunkService chunkService,
            GameContext game
        ) : base(generationResourcesProvider, distanceCalculationService, chunkSegmentService, checkpointService, chunkService, game)
        {
            SequentialObstacleGenerationService = sequentialObstacleGenerationService;
            SequentialCollectableSpawnService = sequentialCollectableSpawnService;
            SequentialWallsGenerationService = sequentialWallsGenerationService;
        }

        public override async UniTask GenerateFirstSequence(
            SequenceParameters parameters,
            Vector3 offset,
            CancellationToken token
        )
        {
            var chunkType = parameters.ChunkType;
            var chunkEntity = await ChunkService.CreateChunk(chunkType, token, positionOffset: offset);

            LastChunkUid = chunkEntity.Uid.Value;

            var chunksInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo;

            var generateChunkData = new GenerateChunkData
            {
                ChunkInfo = chunksInfo[chunkType],
                NewChunk = chunkEntity.Link.View as ChunkView,
                NewChunkEntity = chunkEntity,
                NewChunkType = chunkType,
            };

            await GenerateSequentially(parameters, generateChunkData, token);
        }

        protected async UniTask GenerateSequentially(
            SequenceParameters targetSequence,
            GenerateChunkData data,
            CancellationToken token
        )
        {
            var chunkEntity = data.NewChunkEntity;
            chunkEntity.IsSequential = true;
            chunkEntity.ReplaceSequenceName(targetSequence.name);

            if (data.ChunkInfo.IsGenerative)
                await SequentialWallsGenerationService.GenerateWalls(
                    data.NewChunk,
                    data.NewChunkType,
                    targetSequence.WallsSettings,
                    token
                );

            var pointNodes = ChunkSegmentService.RegisterChunk(chunkEntity);
            DistanceCalculationService.CalculateDistances(ChunkSegmentService.Segments);

            await SequentialObstacleGenerationService.GenerateObstacles(
                data.NewChunk,
                data.NewChunkType,
                targetSequence,
                pointNodes,
                token
            );

            if (targetSequence.ShouldSpawnCollectables)
                SequentialCollectableSpawnService.SpawnCollectables(targetSequence, data.NewChunk);

            foreach (var node in pointNodes)
                CheckpointService.TrySpawn(node.StartDistance, node.EndDistance);
        }
    }
}