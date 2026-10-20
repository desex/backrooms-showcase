using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Sequence;

namespace Game.Services.Chunks
{
    public interface IWallService
    {
        UniTask<(ChunkWallView leftWall, ChunkWallView rightWall)> CreateWallPair(
            EChunkWallType leftWallType,
            EChunkWallType rightWallType,
            CancellationToken token
        );

        UniTask<(ChunkWallView leftWall, ChunkWallView rightWall)> CreateSequenceWallPair(
            WallPairTypes wallPairTypes,
            CancellationToken token
        );

        UniTask DespawnWallPair(ChunkWallView wall1, ChunkWallView wall2, CancellationToken token);
        void ResetService();
    }
}