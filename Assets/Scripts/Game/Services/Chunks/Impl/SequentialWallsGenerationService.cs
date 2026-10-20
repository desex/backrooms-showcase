using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Sequence;

namespace Game.Services.Chunks.Impl
{
    public class SequentialWallsGenerationService : ISequentialWallsGenerationService
    {
        private readonly IWallService _wallService;

        public SequentialWallsGenerationService(IWallService wallService)
        {
            _wallService = wallService;
        }

        public async UniTask GenerateWalls(
            ChunkView chunk,
            EChunkType chunkType,
            List<WallPairTypes> walls,
            CancellationToken token
        )
        {
            switch (chunkType)
            {
                case EChunkType.Straight:
                    await GenerateStraightChunkWalls(chunk, walls, token);
                    break;
            }
        }

        private async UniTask GenerateStraightChunkWalls(
            ChunkView chunk,
            List<WallPairTypes> walls,
            CancellationToken token
        )
        {
            var wallsBuilder = new StraightChunkBuilder(chunk);

            foreach (var wallPairTypes in walls)
            {
                var (leftWall, rightWall) = await _wallService.CreateSequenceWallPair(wallPairTypes, token);
                wallsBuilder.AddWalls(leftWall, rightWall);
            }

            wallsBuilder.BuildChunkAnchors();
        }
    }
}