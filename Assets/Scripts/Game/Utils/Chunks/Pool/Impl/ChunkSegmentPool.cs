using System;
using System.Collections.Generic;
using Game.Utils.DistanceCalculation;

namespace Game.Utils.Chunks.Pool.Impl
{
    public class ChunkSegmentPool : IChunksSegmentPool
    {
        private readonly Queue<ChunkSegment> _segments = new();
        
        public ChunkSegment Get()
        {
            return _segments.TryDequeue(out var segment) 
                ? segment 
                : null;
        }

        public void Return(ChunkSegment chunkSegment)
        {
            if (_segments.Contains(chunkSegment))
                throw new Exception(nameof(ChunkSegmentPool) + ".cs: Segment is already in pool");
            
            chunkSegment.Reset();
            _segments.Enqueue(chunkSegment);
        }
    }
}