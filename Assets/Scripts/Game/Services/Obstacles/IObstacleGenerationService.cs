using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;

namespace Game.Services.Obstacles
{
    public interface IObstacleGenerationService
    {
        int CurrentGroupDistanceIndex { get; set; }
        int CurrentSubGroupDistanceIndex { set; }

        UniTask GenerateObstacles(
            ChunkView chunkView,
            EChunkType chunkType,
            IReadOnlyList<string> generatedChunkData,
            List<ChunkSegment> pointNodes,
            CancellationToken token
        );

        void ResetObstaclesProgression();
    }
}