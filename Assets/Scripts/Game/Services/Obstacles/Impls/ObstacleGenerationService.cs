using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Checkpoints;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Providers;
using Game.Services.DistanceCalculation;
using Game.Services.Providers;
using Game.Services.Random;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Game.Utils.Obstacles;
using PdUtils.RandomProvider;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Services.Obstacles.Impls
{
    public sealed class ObstacleGenerationService : IObstacleGenerationService
    {
        private static readonly EObstacleType[] _allObstacleTypes =
            (EObstacleType[])Enum.GetValues(typeof(EObstacleType));

        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IObstacleService _obstacleService;
        private readonly IObstacleSpawnValidatorService _obstacleSpawnValidatorService;
        private readonly IRandomProvider _randomProvider;
        private readonly IRandomService _randomService;

        private int _obstaclesProgressStartInMeters;

        public ObstacleGenerationService(
            IObstacleService obstacleService,
            IRandomProvider randomProvider,
            IRandomService randomService,
            GameContext game,
            IGenerationResourcesProvider generationResourcesProvider,
            IGameFieldProvider gameFieldProvider,
            IDistanceCalculationService distanceCalculationService,
            IObstacleSpawnValidatorService obstacleSpawnValidatorService,
            ICheckpointsParameters checkpointsParameters,
            IChunkSegmentService chunkSegmentService
        )
        {
            _obstacleService = obstacleService;
            _randomProvider = randomProvider;
            _randomService = randomService;
            _game = game;
            _generationResourcesProvider = generationResourcesProvider;
            _gameFieldProvider = gameFieldProvider;
            _distanceCalculationService = distanceCalculationService;
            _obstacleSpawnValidatorService = obstacleSpawnValidatorService;
            _checkpointsParameters = checkpointsParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        public int CurrentGroupDistanceIndex { get; set; } = 0;
        public int CurrentSubGroupDistanceIndex { get; set; } = 0;

        public async UniTask GenerateObstacles(
            ChunkView chunkView,
            EChunkType chunkType,
            IReadOnlyList<string> generatedWallPresets,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            var chunkInfo = _generationResourcesProvider.ChunkGenerationSettings.ChunksInfo[chunkType];

            if (chunkInfo.IsGenerative)
                await GenerateObstaclesInGenerativeChunk(chunkView, chunkInfo, generatedWallPresets, pointNodes, token);
            else
                await GenerateObstaclesInNonGenerativeChunk(chunkView, chunkInfo, pointNodes, token);
        }

        public void ResetObstaclesProgression()
        {
            var player = _game.PlayerEntity;
            _obstaclesProgressStartInMeters = player.PassedDistanceInMeters.Value;
        }

        private async UniTask GenerateObstaclesInGenerativeChunk(
            ChunkView chunkView,
            ChunkTypeInfo chunkInfo,
            IReadOnlyList<string> generatedWallPresets,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            using var _ = ListPool<ObstacleSpawnInfo>.Get(out var obstacleSpawnInfos);

            for (var i = 1; i < generatedWallPresets.Count; i++)
            {
                var presetRules =
                    _generationResourcesProvider.ChunkGenerationSettings.WallPresets[generatedWallPresets[i]];

                if (!presetRules.CanSpawnObstacles)
                    continue;

                var wall = chunkView.ChunkWalls[i * 2];
                var wallTransform = wall.transform;
                var wallStartPoint = wallTransform.position;
                var wallEndPoint = wall.EndAnchor.position;
                var wallLength = Vector3.Distance(wallStartPoint, wallEndPoint);
                var wallForward = wallTransform.forward;
                var position = wallStartPoint + wallForward * wallLength / 2;

                var spawnInfo = new ObstacleSpawnInfo(position, wallTransform.right, wallTransform.rotation, 2, 2, i);

                obstacleSpawnInfos.Add(spawnInfo);
            }

            using var filteredSpawnInfosDisposable = FilterSpawnPoints(out var filteredSpawnInfos, obstacleSpawnInfos);

            var obstaclesTypes = chunkInfo.AvailableObstacles;

            await GenerateObstacles(
                chunkView,
                filteredSpawnInfos,
                filteredSpawnInfos.Count,
                obstaclesTypes,
                pointNodes[^1],
                pointNodes,
                token
            );
        }

        private async UniTask GenerateObstaclesInNonGenerativeChunk(
            ChunkView chunkView,
            ChunkTypeInfo chunkInfo,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            if (chunkView.ObstacleSpawnPoints.Length == 0)
                return;

            if (chunkInfo.ObstaclesCount.y == 0)
                return;

            var obstacleTypes = chunkInfo.AvailableObstacles;

            using var obstacleSpawnInfosDisposable = ListPool<ObstacleSpawnInfo>.Get(out var obstacleSpawnInfos);

            foreach (var spawnPoint in chunkView.ObstacleSpawnPoints)
                obstacleSpawnInfos.Add(spawnPoint);

            using var filteredSpawnInfosDisposable = FilterSpawnPoints(out var filteredSpawnInfos, obstacleSpawnInfos);
            var obstaclesCount = filteredSpawnInfos.Count;

            await GenerateObstacles(
                chunkView,
                filteredSpawnInfos,
                obstaclesCount,
                obstacleTypes,
                pointNodes[^1],
                pointNodes,
                token
            );
        }

        private IDisposable FilterSpawnPoints(
            out IReadOnlyList<ObstacleSpawnInfo> filteredSpawnInfos,
            IReadOnlyList<ObstacleSpawnInfo> obstacleSpawnInfos
        )
        {
            var player = _game.PlayerEntity;
            var playerPassedDistance = player.PassedDistanceInMeters.Value - _obstaclesProgressStartInMeters;
            var obstacleRules = _generationResourcesProvider.ChunkGenerationSettings.DistanceToObstaclesRules;

            var obstacleCount = GetObstacleCount(playerPassedDistance, obstacleRules);

            var filteredSpawnInfosDisposable = AddFilteredSpawnPositions(
                out filteredSpawnInfos,
                obstacleCount,
                obstacleSpawnInfos
            );

            return filteredSpawnInfosDisposable;
        }

        private IDisposable AddFilteredSpawnPositions(
            out IReadOnlyList<ObstacleSpawnInfo> filteredSpawnInfos,
            int obstacleCount,
            IReadOnlyList<ObstacleSpawnInfo> obstacleSpawnInfos
        )
        {
            var filteredSpawnInfosDisposable = ListPool<ObstacleSpawnInfo>.Get(out var filteredSpawnInfosList);
            int index;

            switch (obstacleCount)
            {
                case 1:
                    index = _randomProvider.Range(0, obstacleSpawnInfos.Count);
                    filteredSpawnInfosList.Add(obstacleSpawnInfos[index]);
                    break;

                case 2:
                    index = _randomProvider.Range(1, obstacleSpawnInfos.Count - 1);
                    filteredSpawnInfosList.Add(obstacleSpawnInfos[index - 1]);
                    filteredSpawnInfosList.Add(obstacleSpawnInfos[index + 1]);
                    break;

                case 3:
                    for (var i = 0; i < obstacleSpawnInfos.Count; i += 2)
                        filteredSpawnInfosList.Add(obstacleSpawnInfos[i]);

                    break;

                case 4:
                    index = _randomProvider.Range(0, obstacleSpawnInfos.Count);

                    for (var i = 0; i < obstacleSpawnInfos.Count; i++)
                    {
                        if (i == index)
                            continue;

                        filteredSpawnInfosList.Add(obstacleSpawnInfos[i]);
                    }

                    break;

                case 5:
                    foreach (var obstacleSpawnInfo in obstacleSpawnInfos)
                        filteredSpawnInfosList.Add(obstacleSpawnInfo);

                    break;

                default:
                    throw new Exception("Unknown obstacle count: " + obstacleCount);
            }

            filteredSpawnInfos = filteredSpawnInfosList;
            return filteredSpawnInfosDisposable;
        }

        private int GetObstacleCount(int playerPassedDistance, ObstaclesRules obstacleRules)
        {
            if (playerPassedDistance <= obstacleRules.DistanceRange.x)
                return
                    _randomProvider.Range(
                        2,
                        4 + 1
                    ); // 2 - 4 obstacles for the first distance range. should be in SO config soon

            if (playerPassedDistance <= obstacleRules.DistanceRange.y)
                return
                    _randomProvider.Range(
                        3,
                        5 + 1
                    ); // 3 - 5 obstacles for the second distance range. should be in SO config soon

            return 5; // 5 obstacles for the third distance range. should be in SO config soon
        }

        private async UniTask GenerateObstacles(
            ChunkView chunkView,
            IReadOnlyList<ObstacleSpawnInfo> obstacleSpawnInfos,
            int obstaclesCount,
            IReadOnlyDictionary<EObstacleType, int> obstacleTypes,
            ChunkSegment lastNode,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            for (var i = 0; i < obstacleSpawnInfos.Count; i++)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);

                _obstacleSpawnValidatorService.FilterObstaclePointInDefaultGeneration(pointNodes);

                if (obstaclesCount == 0)
                    return;

                var isSpawn = obstacleSpawnInfos.Count - i <= obstaclesCount || _randomProvider.Range(0, 2) == 0;

                if (!isSpawn)
                    continue;

                var obstacleSpawnInfo = obstacleSpawnInfos[i];
                var currentObstacleType = GetNextObstacleType(obstacleTypes);

                if (!_generationResourcesProvider.ObstaclesBase.Obstacles.ContainsKey(currentObstacleType))
                    continue;

                var isWallObstacle = _generationResourcesProvider.ObstaclesBase.ObstacleInfos[currentObstacleType]
                    .IsObstacleWithWalls;

                var offset = Vector3.zero;
                Vector3 position;
                Quaternion rotation;

                if (!isWallObstacle)
                {
                    var leftOffset = obstacleSpawnInfo.LeftOffset;
                    var rightOffset = obstacleSpawnInfo.RightOffset;

                    var isLeftOffset = _randomProvider.Range(0, 2) == 0;

                    offset = isLeftOffset
                        ? -obstacleSpawnInfo.Right * _randomProvider.Range(0, leftOffset)
                        : obstacleSpawnInfo.Right * _randomProvider.Range(0, rightOffset);

                    position = obstacleSpawnInfo.Position;
                    rotation = obstacleSpawnInfo.Rotation;

                    if (CanSpawnObstacle(position))
                    {
                        obstaclesCount--;
                        continue;
                    }
                }
                else
                {
                    var wallIndex = obstacleSpawnInfo.WallIndex!.Value;

                    var leftWall = chunkView.ChunkWalls[wallIndex * 2];
                    var rightWall = chunkView.ChunkWalls[wallIndex * 2 + 1];

                    var leftWallTransform = leftWall.transform;
                    position = leftWallTransform.position;
                    rotation = leftWallTransform.rotation;

                    if (CanSpawnObstacle(position))
                    {
                        obstaclesCount--;
                        continue;
                    }

                    leftWall.gameObject.SetActive(false);
                    rightWall.gameObject.SetActive(false);
                }

                obstaclesCount--;

                var obstacle = await _obstacleService.CreateObstacle(
                    currentObstacleType,
                    position,
                    offset,
                    rotation,
                    token,
                    _gameFieldProvider.GameField.ObstaclesRoot
                );

                chunkView.Obstacles.Add(obstacle.obstacleGroupView);

                var obstacleEndPoint = obstacle.obstacleGroupView.EndPoint;

                if (isWallObstacle && obstacleEndPoint)
                {
                    var wallIndex = obstacleSpawnInfo.WallIndex!.Value;
                    var endWallPosition = obstacleEndPoint.position;

                    for (var j = wallIndex * 2 + 2; j < chunkView.ChunkWalls.Count; j += 2)
                    {
                        var leftWall = chunkView.ChunkWalls[j];
                        var rightWall = chunkView.ChunkWalls[j + 1];

                        leftWall.transform.position = endWallPosition;
                        rightWall.transform.position = endWallPosition;

                        endWallPosition = rightWall.EndAnchor.position;
                    }

                    chunkView.EndOnlyAnchor.position = endWallPosition;

                    var distance = _distanceCalculationService.GetDistance(chunkView.StartAnchor.position, _chunkSegmentService.Segments);
                    _chunkSegmentService.CorrectEndAnchor(
                        lastNode,
                        chunkView.StartAnchor.position,
                        chunkView.EndOnlyAnchor.position,
                        distance
                    );
                }
            }
        }

        private EObstacleType GetNextObstacleType(IReadOnlyDictionary<EObstacleType, int> obstacleTypes)
        {
            var currentAvailableTypesInGroup = _generationResourcesProvider.ChunkGenerationSettings
                .ObstacleDistanceGroupsList[CurrentGroupDistanceIndex]
                .obstacleDistanceGroups
                .ObstaclesSubGroups[CurrentSubGroupDistanceIndex];

            using var _ = DictionaryPool<EObstacleType, int>.Get(out var obstaclesToSpawnFilter);
            obstacleTypes.CopyTo(obstaclesToSpawnFilter);

            foreach (var availableObstacleType in _allObstacleTypes)
            {
                if (availableObstacleType == EObstacleType.Everything)
                    continue;

                if (currentAvailableTypesInGroup.HasFlag(availableObstacleType))
                    continue;

                if (obstaclesToSpawnFilter.ContainsKey(availableObstacleType))
                    obstaclesToSpawnFilter.Remove(availableObstacleType);
            }

            var nextObstacleType = _randomService.GetWeightedRandom(obstaclesToSpawnFilter);

            return nextObstacleType;
        }

        private bool CanSpawnObstacle(Vector3 position)
        {
            var radius = _checkpointsParameters.RadiusObstacleSpawnCheckInDefault;

            var checkpointSpawnDistance = _obstacleSpawnValidatorService.CheckpointSpawnDistanceInDefaultGeneration;

            return _obstacleSpawnValidatorService.IsPointInRadius(position, radius, checkpointSpawnDistance);
        }
    }
}