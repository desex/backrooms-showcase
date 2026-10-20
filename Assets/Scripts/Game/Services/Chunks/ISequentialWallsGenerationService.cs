using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Sequence;

namespace Game.Services.Chunks
{
    public interface ISequentialWallsGenerationService
    {
        UniTask GenerateWalls(
            ChunkView chunk,
            EChunkType chunkType,
            List<WallPairTypes> walls,
            CancellationToken token
        );
    }
}