using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Checkpoints;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Providers;
using Game.Services.Chunks;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.Obstacles.Impls
{
    public class SequentialObstacleGenerationService : ISequentialObstacleGenerationService, IDisposable
    {
        private readonly ICheckpointsParameters _checkpointsParameters;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IObstacleSpawnValidatorService _obstacleSpawnValidatorService;
        private readonly ISequentialObstacleService _sequentialObstacleService;
        private readonly CancellationTokenSource _token = new CancellationTokenSource();

        public SequentialObstacleGenerationService(
            ISequentialObstacleService sequentialObstacleService,
            IGameFieldProvider gameFieldProvider,
            IObstacleSpawnValidatorService obstacleSpawnValidatorService,
            ICheckpointsParameters checkpointsParameters
        )
        {
            _sequentialObstacleService = sequentialObstacleService;
            _gameFieldProvider = gameFieldProvider;
            _obstacleSpawnValidatorService = obstacleSpawnValidatorService;
            _checkpointsParameters = checkpointsParameters;
        }

        public void Dispose()
        {
            _token?.Cancel();
        }

        public async UniTask GenerateObstacles(
            ChunkView chunkView,
            EChunkType chunkType,
            SequenceParameters targetSequence,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            switch (chunkType)
            {
                case EChunkType.Straight:
                    await GenerateObstaclesInGenerativeChunkSequentially(chunkView, targetSequence, pointNodes, token);
                    break;

                case EChunkType.None:
                    Debug.LogError($"[{nameof(SequentialObstacleGenerationService)}] Chunk type is none");
                    break;

                default:
                    await GenerateObstaclesInChunkSequentially(chunkView, targetSequence, token);
                    break;
            }
        }

        private async UniTask GenerateObstaclesInChunkSequentially(
            ChunkView chunkView,
            SequenceParameters targetSequence,
            CancellationToken token
        )
        {
            var startСhunkAnchorPosition = chunkView.StartAnchor.position;
            var targetSequenceChunkSettings = targetSequence.UniquechunkObstacleSettings;
            var chunkViewRotation = chunkView.transform.rotation;

            for (var i = 0; i < targetSequenceChunkSettings.Count; i++)
            {
                var chunkSequenceParameterSetting = targetSequenceChunkSettings[i];

                var obstacleSpawnPosition =
                    startСhunkAnchorPosition + chunkViewRotation * chunkSequenceParameterSetting.Offset;

                var offsetRotationEuler = Quaternion.Euler(chunkSequenceParameterSetting.Rotation);
                var spawnRotation = chunkViewRotation * offsetRotationEuler;

                if (chunkSequenceParameterSetting.ObstaclePrefab == null)
                    continue;

                var targetObstacleType = chunkSequenceParameterSetting.ObstacleType;
                var targetObstacleID = chunkSequenceParameterSetting.ObstaclePrefab.gameObject.GetInstanceID();

                var (obstacleEntity, obstacleGroupView) = await _sequentialObstacleService.CreateObstacle(
                    targetObstacleType,
                    targetObstacleID,
                    obstacleSpawnPosition,
                    spawnRotation,
                    token,
                    _gameFieldProvider.GameField.ObstaclesRoot
                );

                if (chunkSequenceParameterSetting.RespawnParameters.IsSetUpRespawn)
                    ChangeRevive(
                        obstacleEntity,
                        obstacleGroupView,
                        chunkSequenceParameterSetting.RespawnParameters.Position
                    );

                chunkView.Obstacles.Add(obstacleGroupView);
            }
        }

        private static void ChangeRevive(
            GameEntity obstacleEntity,
            ObstacleGroupView startСhunkAnchorPosition,
            Vector3 respawnOffset
        )
        {
            var reviveOffset = startСhunkAnchorPosition.transform.TransformPoint(respawnOffset);
            obstacleEntity.ReplaceReviveOffset(reviveOffset);
        }

        private async UniTask GenerateObstaclesInGenerativeChunkSequentially(
            ChunkView chunkView,
            SequenceParameters targetSequence,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        )
        {
            var pointsCount = targetSequence.ObstaclesSettings.Count;
            var obstaclesSettings = targetSequence.ObstaclesSettings;

            var chunkStart = chunkView.StartAnchor.position;

            var chunkTransform = chunkView.transform;
            var chunkRotation = chunkTransform.rotation;
            var obstaclesSpawnPoints = new Vector3[pointsCount];

            AssignObstaclesPoints();

            _obstacleSpawnValidatorService.FilterObstaclePointInSequential(pointNodes);

            await GenerateObstacles(obstaclesSpawnPoints, chunkRotation, targetSequence, chunkView, token);

            await GenerateReplaceObstacles(token);

            return;

            void AssignObstaclesPoints()
            {
                for (var i = 0; i < pointsCount; i++)
                {
                    var sequencePoint = obstaclesSettings[i].PointsSettings;
                    var basicPointOffset = sequencePoint.IsNeedOffset ? sequencePoint.Offset : Vector3.zero;
                    var targetPointOffset = chunkRotation * basicPointOffset;
                    obstaclesSpawnPoints[i] = chunkStart + targetPointOffset;
                }
            }

            async UniTask GenerateReplaceObstacles(CancellationToken token)
            {
                var replaceObstacles = targetSequence.WallsSettings;
                var chunkWalls = chunkView.ChunkWalls;

                for (var i = 0; i < replaceObstacles.Count; i++)
                {
                    if (!replaceObstacles[i].IsReplaceWallsWithObstacles)
                        continue;

                    if (chunkWalls.Count == 0)
                        return;

                    var targetType = replaceObstacles[i].ReplaceObstacle.ObstacleType;

                    var targetID = replaceObstacles[i]
                        .ReplaceObstacle
                        .ObstaclePrefab
                        .GetInstanceID();

                    var leftWall = chunkWalls[i * 2];
                    var rightWall = chunkWalls[i * 2 + 1];
                    var leftWallTransform = leftWall.transform;
                    var position = leftWallTransform.position;
                    var checkpointSpawnDistance = _obstacleSpawnValidatorService.CheckpointSpawnDistanceInSequential;
                    var radius = _checkpointsParameters.RadiusObstacleSpawnCheckInSequential;

                    if (_obstacleSpawnValidatorService.IsPointInRadius(position, radius, checkpointSpawnDistance))
                        continue;

                    var (obstacleEntity, obstacleGroupView) = await _sequentialObstacleService.CreateObstacle(
                        targetType,
                        targetID,
                        position,
                        chunkRotation,
                        token
                    );

                    chunkView.Obstacles.Add(obstacleGroupView);

                    leftWall.gameObject.SetActive(false);
                    rightWall.gameObject.SetActive(false);
                }
            }
        }

        private async UniTask GenerateObstacles(
            IReadOnlyList<Vector3> obstacleSpawnPoints,
            Quaternion rotation,
            SequenceParameters targetSequence,
            ChunkView chunkView,
            CancellationToken token
        )
        {
            var count = Mathf.Min(obstacleSpawnPoints.Count, targetSequence.ObstaclesSettings.Count);

            for (var i = 0; i < count; i++)
            {
                var obstacleSetting = targetSequence.ObstaclesSettings[i];

                if (obstacleSetting.ObstaclePrefab == null)
                    continue;

                var obstacleSpawnPosition = obstacleSpawnPoints[i];
                var offsetRotationEuler = Quaternion.Euler(obstacleSetting.PointsSettings.Rotation);
                var targetRotation = rotation * offsetRotationEuler;

                var checkpointSpawnDistance = _obstacleSpawnValidatorService.CheckpointSpawnDistanceInSequential;
                var radius = _checkpointsParameters.RadiusObstacleSpawnCheckInDefault;

                if (_obstacleSpawnValidatorService.IsPointInRadius(
                    obstacleSpawnPosition,
                    radius,
                    checkpointSpawnDistance
                ))
                    continue;

                var targetObstacleType = obstacleSetting.ObstacleType;
                var targetObstacleID = obstacleSetting.ObstaclePrefab.gameObject.GetInstanceID();

                var (obstacleEntity, obstacleGroupView) = await _sequentialObstacleService.CreateObstacle(
                    targetObstacleType,
                    targetObstacleID,
                    obstacleSpawnPosition,
                    targetRotation,
                    token,
                    _gameFieldProvider.GameField.ObstaclesRoot
                );

                if (obstacleSetting.RespawnParameters.IsSetUpRespawn)
                    ChangeRevive(obstacleEntity, obstacleGroupView, obstacleSetting.RespawnParameters.Position);

                chunkView.Obstacles.Add(obstacleGroupView);
            }
        }
    }
}