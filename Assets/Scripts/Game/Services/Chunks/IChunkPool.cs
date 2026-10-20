using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Generation;

namespace Game.Services.Chunks
{
    public interface IChunkPool : IViewPool<ChunkView>
    {
        (ChunkView chunk, int randomIndex) SpawnChunk(EChunkType chunkType);
        void DespawnChunk(EChunkType chunkType, ChunkView chunk, int randomIndex);
    }
}