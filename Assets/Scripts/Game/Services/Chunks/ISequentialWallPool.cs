using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Generation;

namespace Game.Services.Chunks
{
    public interface ISequentialWallPool : IViewPool<ChunkWallView>
    {
        ChunkWallView SpawnWall(int obstacleID);
        void DespawnWall(int obstacleID, ChunkWallView wall);
    }
}