using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Services.Chunks
{
    public interface IChunkService
    {
        UniTask<GameEntity> CreateChunk(
            EChunkType chunkType,
            CancellationToken token,
            Transform previousChunkEndAnchor = null,
            GameEntity previousChunk = null,
            Vector3? positionOffset = null
        );

        UniTask DeactivateChunk(ChunkNode rootNode, CancellationToken token);
        UniTask DeactivateChunk(GameEntity chunk, CancellationToken token);
    }
}