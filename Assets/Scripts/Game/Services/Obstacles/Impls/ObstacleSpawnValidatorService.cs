using System.Collections.Generic;
using Db.Checkpoints;
using Db.Score;
using Game.Services.DistanceCalculation;
using Game.Utils.Checkpoint;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.Obstacles.Impls
{
    public class ObstacleSpawnValidatorService : IObstacleSpawnValidatorService
    {
        private readonly ICheckpointsParameters _checkpointsParameters;

        private readonly List<float> _checkpointSpawnDistance = new List<float>();
        private readonly List<float> _checkpointSpawnDistanceInSequential = new List<float>();
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IScoreParameters _scoreParameters;

        public ObstacleSpawnValidatorService(
            IDistanceCalculationService distanceCalculationService,
            ICheckpointsParameters checkpointsParameters,
            IScoreParameters scoreParameters,
            IChunkSegmentService chunkSegmentService
        )
        {
            _distanceCalculationService = distanceCalculationService;
            _checkpointsParameters = checkpointsParameters;
            _scoreParameters = scoreParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        public IReadOnlyList<float> CheckpointSpawnDistanceInSequential => _checkpointSpawnDistanceInSequential;
        public IReadOnlyList<float> CheckpointSpawnDistanceInDefaultGeneration => _checkpointSpawnDistance;

        public void FilterObstaclePointInDefaultGeneration(List<ChunkSegment> pointNodes)
        {
            _checkpointSpawnDistance.Clear();

            foreach (var segment in pointNodes)
            {
                var listDistancesInNodes = CheckpointUtils.CalculateDistancesForSpawn(
                    segment.StartDistance,
                    segment.EndDistance,
                    _checkpointsParameters.FrequencyCheckpointsInMeters,
                    _scoreParameters.DistanceToMetersRatio
                );

                _checkpointSpawnDistance.AddRange(listDistancesInNodes);
            }
        }

        public void FilterObstaclePointInSequential(List<ChunkSegment> pointNodes)
        {
            _checkpointSpawnDistanceInSequential.Clear();

            foreach (var node in pointNodes)
            {
                var listDistancesInNodes = CheckpointUtils.CalculateDistancesForSpawn(
                    node.StartDistance,
                    node.EndDistance,
                    _checkpointsParameters.FrequencyCheckpointsInMeters,
                    _scoreParameters.DistanceToMetersRatio
                );

                _checkpointSpawnDistanceInSequential.AddRange(listDistancesInNodes);
            }
        }

        public bool IsPointInRadius(Vector3 obstaclePoint, float radius, IReadOnlyList<float> distances)
        {
            var radiusSquared = radius * radius;

            foreach (var distance in distances)
            {
                if (!_distanceCalculationService.TryGetPositions(distance, out var spawnPositions, _chunkSegmentService.Segments))
                    continue;

                foreach (var spawnPoint in spawnPositions)
                {
                    var checkpointPosition = spawnPoint.Position;

                    var offset = obstaclePoint - checkpointPosition;
                    var distanceSquared = offset.sqrMagnitude;

                    if (distanceSquared <= radiusSquared)
                        return true;
                }
            }

            return false;
        }
    }
}