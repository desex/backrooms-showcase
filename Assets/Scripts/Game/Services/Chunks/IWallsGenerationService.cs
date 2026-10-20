using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;

namespace Game.Services.Chunks
{
    public interface IWallsGenerationService
    {
        UniTask GenerateWalls(
            ChunkView chunk,
            EChunkType chunkType,
            ICollection<string> generatedWallPresets,
            CancellationToken token
        );
    }
}