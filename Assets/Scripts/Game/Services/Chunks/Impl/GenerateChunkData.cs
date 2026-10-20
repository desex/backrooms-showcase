using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;

namespace Game.Services.Chunks.Impl
{
    public struct GenerateChunkData
    {
        public ChunkTypeInfo ChunkInfo;
        public ChunkView NewChunk;
        public GameEntity NewChunkEntity;
        public EChunkType NewChunkType;
    }
}