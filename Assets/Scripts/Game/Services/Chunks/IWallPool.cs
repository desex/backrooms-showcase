using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Generation;

namespace Game.Services.Chunks
{
    public interface IWallPool : IViewPool<ChunkWallView>
    {
        (ChunkWallView wall, int randomIndex) SpawnWall(EChunkWallType wallType);
        void DespawnWall(EChunkWallType wallType, ChunkWallView wall, int randomIndex);
    }
}