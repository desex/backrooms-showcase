using Game.Utils.DistanceCalculation;

namespace Game.Utils.Chunks
{
    public readonly struct ClearWayData
    {
        public readonly GameEntity ChunkEntity;
        public readonly EChunkSide ChunkSide;
        public readonly float MaxHeightCollectables;
        
        public ClearWayData(GameEntity chunkEntity, EChunkSide chunkSide, float maxHeightCollectables)
        {
            ChunkEntity = chunkEntity;
            ChunkSide = chunkSide;
            MaxHeightCollectables = maxHeightCollectables;
        }
    }
}