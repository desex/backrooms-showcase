using System.Collections.Generic;
using Game.Utils.DistanceCalculation;
using UnityEngine;

namespace Game.Services.DistanceCalculation
{
    public interface IChunkSegmentService
    {
        IReadOnlyList<ChunkSegment> Segments { get; }
        public ChunkSegment LastChunkSegment { get; }
        void SetStartOffset(float offset);
        List<ChunkSegment> RegisterChunk(GameEntity chunkEntity);
        void ApplyDistanceOffset(float offset);
        void UnregisterChunk(GameEntity chunkEntity);
        void CorrectEndAnchor(ChunkSegment segment, Vector3 startPosition, Vector3 next1Position, float startDistance);
    }
}