using System.Collections.Generic;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.Obstacles
{
    public interface IObstacleSpawnValidatorService
    {
        public IReadOnlyList<float> CheckpointSpawnDistanceInSequential { get; }
        public IReadOnlyList<float> CheckpointSpawnDistanceInDefaultGeneration { get; }

        void FilterObstaclePointInSequential(List<ChunkSegment> pointNodes);
        void FilterObstaclePointInDefaultGeneration(List<ChunkSegment> pointNodes);
        bool IsPointInRadius(Vector3 obstaclePoint, float radius, IReadOnlyList<float> distances);
    }
}