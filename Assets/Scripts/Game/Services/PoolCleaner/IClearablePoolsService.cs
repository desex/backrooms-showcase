using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils.Chunks;

namespace Game.Services.PoolCleaner
{
    public interface IClearablePoolsService
    {
        void CreateAllPools();
        UniTask ClearAllPools(ChunkNode node, CancellationToken token);
    }
}