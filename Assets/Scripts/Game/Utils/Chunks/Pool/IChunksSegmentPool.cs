using Game.Utils.DistanceCalculation;

namespace Game.Utils.Chunks.Pool
{
    public interface IChunksSegmentPool
    {
        ChunkSegment Get();
        void Return(ChunkSegment chunkSegment);
    }
}