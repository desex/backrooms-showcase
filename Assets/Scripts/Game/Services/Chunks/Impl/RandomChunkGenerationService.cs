using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Score;
using Db.TransitionChunk;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.CheckPoints;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Services.Providers;
using Game.Services.Random;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Game.Utils.Overlaps;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Chunks.Impl
{
    public sealed class RandomChunkGenerationService : AChunkGenerationService
    {
        private static readonly EChunkType[] _allChunkTypes = (EChunkType[])Enum.GetValues(typeof(EChunkType));
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IObstacleGenerationService _obstacleGenerationService;
        private readonly IOverlapService _overlapService;
        private readonly IRandomService _randomService;
        private readonly IScoreParameters _scoreParameters;
        private readonly ITransitionChunkParameters _transitionChunkParameters;
        private readonly IWallsGenerationService _wallsGenerationService;

        private Dictionary<EChunkType, int> _availableChunks;
        private float _targetTransitDistance;

        public RandomChunkGenerationService(
            IGenerationResourcesProvider generationResourcesProvider,
            IDistanceCalculationService distanceCalculationService,
            IChunkSegmentService chunkSegmentService,
            ITransitionChunkParameters transitionChunkParameters,
            IObstacleGenerationService obstacleGenerationService,
            IWallsGenerationService wallsGenerationService,
            ICheckpointService checkpointService,
            IScoreParameters scoreParameters,
            IOverlapService overlapService,
            IRandomService randomService,
            IChunkService chunkService,
            GameContext game
        ) : base(generationResourcesProvider, distanceCalculationService, chunkSegmentService, checkpointService, chunkService, game)
        {
            _chunkSegmentService = chunkSegmentService;
            _obstacleGenerationService = obstacleGenerationService;
            _transitionChunkParameters = transitionChunkParameters;
            _wallsGenerationService = wallsGenerationService;
            _scoreParameters = scoreParameters;
            _overlapService = overlapService;
            _randomService = randomService;
        }

        public override void Initialize()
        {
            _targetTransitDistance = _transitionChunkParameters.DistanceBetweenChunks;
        }

        private IDisposable GetAvailableChunkTypes(
            GameEntity chunkEntity,
            out Dictionary<EChunkType, int> availableChunkTypes
        )
        {
            var chunkType = chunkEntity.ChunkNode.Value.ChunkType;
            var chunkInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo[chunkType];
            var availableChunkTypesDisposable = DictionaryPool<EChunkType, int>.Get(out availableChunkTypes);

            chunkInfo.ChunksGenerationInfo.NextChunkTypes.CopyTo(availableChunkTypes);

            availableChunkTypes = CheckChunkTypesForSequenceLimit(
                availableChunkTypes,
                chunkEntity.ChunkNode.Value,
                chunkInfo
            );

            availableChunkTypes = CheckChunkTypesForDistance(availableChunkTypes);

            return availableChunkTypesDisposable;
        }

        private static Dictionary<EChunkType, int> CheckChunkTypesForSequenceLimit(
            Dictionary<EChunkType, int> availableChunkTypes,
            ChunkNode node,
            ChunkTypeInfo chunkInfo
        )
        {
            var limitForCurrentChunkType = chunkInfo.ChunksGenerationInfo.ChunkSequenceCount;

            if (node.OrderInSequence < limitForCurrentChunkType)
                return availableChunkTypes;

            ChangeAvailableChunkList();

            return availableChunkTypes;

            void ChangeAvailableChunkList()
            {
                var currentChunkType = node.ChunkType;

                foreach (var pair in availableChunkTypes)
                {
                    if (pair.Key != currentChunkType)
                        continue;

                    availableChunkTypes.Remove(pair.Key);
                    break;
                }
            }
        }

        private Dictionary<EChunkType, int> CheckChunkTypesForDistance(Dictionary<EChunkType, int> availableChunkTypes)
        {
            var typeFilter = GetTypeFilter();
            ChangeAvailableChunkTypesAccordingToTypeFilter();
            ChangeAvailableChunkListIfLastChunksAreIncorrectAccordingToSettings();

            return availableChunkTypes;

            EChunkType GetTypeFilter()
            {
                var passedDistance = Game.PlayerEntity.PassedDistance.Value;
                var currentGroupCap = 0;

                var distanceGroups = GenerationResourcesProvider.ChunkGenerationSettings.DistanceGroups;
                var filter = distanceGroups[^1].GroupFilter;

                for (var i = 0; i < distanceGroups.Count; i++)
                {
                    currentGroupCap += distanceGroups[i].GroupLength;

                    if (passedDistance >= currentGroupCap)
                        continue;

                    filter = distanceGroups[i].GroupFilter;
                    break;
                }

                return filter;
            }

            void ChangeAvailableChunkTypesAccordingToTypeFilter()
            {
                using var _ = ListPool<EChunkType>.Get(out var itemsToRemove);

                foreach (var pair in availableChunkTypes)
                {
                    if ((pair.Key & typeFilter) == EChunkType.None)
                        itemsToRemove.Add(pair.Key);
                }

                foreach (var item in itemsToRemove)
                    availableChunkTypes.Remove(item);
            }

            void ChangeAvailableChunkListIfLastChunksAreIncorrectAccordingToSettings()
            {
                if (availableChunkTypes.Count != 0)
                    return;

                foreach (var chunkType in _allChunkTypes)
                {
                    if ((chunkType & typeFilter) != EChunkType.None && chunkType != EChunkType.Everything)
                        availableChunkTypes.Add(chunkType, 1);
                }
            }
        }

        protected override async UniTask GenerateNextChunk(
            GameEntity chunkEntity,
            Transform endAnchor,
            CancellationToken token
        )
        {
            using var __ = GetAvailableChunkTypes(chunkEntity, out _availableChunks);
            var chunksInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo;

            using var _ = DictionaryPool<EChunkType, int>.Get(out var nonBoundingChunkTypes);

            foreach (var (chunkType, weight) in _availableChunks)
                nonBoundingChunkTypes.Add(chunkType, weight);

            var shouldSpawnTransit = ShouldSpawnTransitChunk();
            _targetTransitDistance += shouldSpawnTransit ? _transitionChunkParameters.DistanceBetweenChunks : 0f;

            var newChunkType = ChooseChunkType();
            var newChunkEntity = await ChunkService.CreateChunk(newChunkType, token, endAnchor, chunkEntity);

            LastChunkUid = newChunkEntity.Uid.Value;

            var generateChunkData = new GenerateChunkData
            {
                ChunkInfo = chunksInfo[newChunkType],
                NewChunk = newChunkEntity.Link.View as ChunkView,
                NewChunkEntity = newChunkEntity,
                NewChunkType = newChunkType,
            };

            await GenerateChunk(generateChunkData, token);

            return;

            EChunkType ChooseChunkType()
            {
                return shouldSpawnTransit ? EChunkType.Transition : GetChunkTypeRandomly();
            }

            bool ShouldSpawnTransitChunk()
            {
                var chunkSpawnPositionInMeters = DistanceCalculationService.GetDistance(endAnchor.position, _chunkSegmentService.Segments)
                                                 * _scoreParameters.DistanceToMetersRatio;

                return chunkSpawnPositionInMeters >= _targetTransitDistance;
            }

            async UniTask GenerateRandomly(GenerateChunkData data, CancellationToken token)
            {
                using var _ = ListPool<string>.Get(out var generatedWallPresets);

                if (data.ChunkInfo.IsGenerative)
                    await _wallsGenerationService.GenerateWalls(
                        data.NewChunk,
                        data.NewChunkType,
                        generatedWallPresets,
                        token
                    );

                var pointNodes = ChunkSegmentService.RegisterChunk(newChunkEntity);
                DistanceCalculationService.CalculateDistances(ChunkSegmentService.Segments);

                await _obstacleGenerationService.GenerateObstacles(
                    data.NewChunk,
                    data.NewChunkType,
                    generatedWallPresets,
                    pointNodes,
                    token
                );

                foreach (var node in pointNodes)
                    CheckpointService.TrySpawn(node.StartDistance, node.EndDistance);
            }

            async UniTask GenerateTransit(GameEntity currentChunkEntity, CancellationToken token)
            {
                var currentChunkNode = currentChunkEntity.ChunkNode.Value;
                var pointNodes = ChunkSegmentService.RegisterChunk(currentChunkEntity);
                DistanceCalculationService.CalculateDistances(ChunkSegmentService.Segments);

                currentChunkNode.IsGenerationStopped = true;

                await UniTask.Yield(PlayerLoopTiming.Update, token);

                foreach (var node in pointNodes)
                    CheckpointService.TrySpawn(node.StartDistance, node.EndDistance);
            }

            EChunkType GetChunkTypeRandomly()
            {
                var chunkType = EChunkType.None;

                while (nonBoundingChunkTypes.Count > 0)
                {
                    chunkType = _randomService.GetWeightedRandom(nonBoundingChunkTypes);

                    var isColliding = IsColliding(chunkType, endAnchor);

                    if (isColliding)
                        nonBoundingChunkTypes.Remove(chunkType);
                    else
                        break;
                }

                return chunkType;
            }

            async UniTask GenerateChunk(GenerateChunkData generateChunkData, CancellationToken token)
            {
                if (shouldSpawnTransit)
                    await GenerateTransit(generateChunkData.NewChunkEntity, token);
                else
                    await GenerateRandomly(generateChunkData, token);
            }
        }

        private bool IsColliding(EChunkType chunkType, Transform endAnchor)
        {
            var chunkInfo = GenerationResourcesProvider.ChunkGenerationSettings.ChunksInfo[chunkType];

            var anchorPosition = endAnchor != null ? endAnchor.position : Vector3.zero;
            var anchorRotation = endAnchor.rotation;
            var anchorForward = endAnchor.forward;
            var collisionBounds = chunkInfo.Chunks[0].CollisionBounds;
            var isNotVeered = anchorForward == Vector3.forward || anchorForward == Vector3.back;

            foreach (var collisionBound in collisionBounds)
            {
                var rotatedCenter = anchorRotation * collisionBound.center;
                var center = anchorPosition + rotatedCenter;

                var collisionBoundSize = collisionBound.size;

                var needSize = isNotVeered
                    ? collisionBoundSize
                    : new Vector3(collisionBoundSize.z, collisionBoundSize.y, collisionBoundSize.x);

                var isColliding = _overlapService.IsOverlapBoxColliding(
                    center,
                    needSize,
                    Quaternion.identity,
                    LayerMasks.Chunk
                );

                if (isColliding)
                    return true;
            }

            return false;
        }
    }
}