using System.Collections.Generic;
using Ecs.Managers;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.DistanceCalculation
{
    public interface IDistanceCalculationService
    {
        void CalculateDistances(IReadOnlyList<ChunkSegment> segments);
        float GetCharacterDistance(Uid characterUid, IEnumerable<ChunkSegment> segments);
        float GetPlayerDistance(IEnumerable<ChunkSegment> segments);
        float GetDistance(Vector3 target, IEnumerable<ChunkSegment> segments);
        bool TryGetPositions(float targetDistance, out List<SpawnPoint> spawnPositions, IEnumerable<ChunkSegment> segments);
        EChunkSide GetChunkSide(Vector3 worldPosition, IEnumerable<ChunkSegment> segments);
    }
}