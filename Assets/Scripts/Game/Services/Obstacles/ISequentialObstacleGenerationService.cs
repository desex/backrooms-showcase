using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;

namespace Game.Services.Obstacles
{
    public interface ISequentialObstacleGenerationService
    {
        UniTask GenerateObstacles(
            ChunkView chunkView,
            EChunkType chunkType,
            SequenceParameters targetSequence,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        );
    }
}