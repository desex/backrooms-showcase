using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Services.Chunks;
using Game.Utils.Chunks;

namespace Game.Services.PoolCleaner.Impl
{
    public class ClearablePoolsService : IClearablePoolsService
    {
        private readonly IChunkService _chunkService;
        private readonly List<IClearablePool> _cleanupMarkers;

        public ClearablePoolsService(List<IClearablePool> cleanupMarkers, IChunkService chunkService)
        {
            _cleanupMarkers = cleanupMarkers;
            _chunkService = chunkService;
        }

        public void CreateAllPools()
        {
            foreach (var marker in _cleanupMarkers)
                marker.CreateSubPools();
        }

        public async UniTask ClearAllPools(ChunkNode node, CancellationToken token)
        {
            await ReturnToPool(node, token);

            foreach (var marker in _cleanupMarkers)
                marker.Clear();
        }

        private async UniTask ReturnToPool(ChunkNode node, CancellationToken token)
        {
            if (node.PreviousNode is not null)
                await ReturnToPool(node.PreviousNode, token);

            await _chunkService.DeactivateChunk(node, token);
        }
    }
}