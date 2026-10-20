using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.Providers;
using Game.Utils.Chunks;
using Game.Utils.Generation;
using Zenject;

namespace Game.Services.Chunks.Impl
{
    public class SequentialWallPool : ASequenceGenerationPool<EChunkWallType, ChunkWallView>, ISequentialWallPool
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;

        protected override IReadOnlyDictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> PoolObjectVariants =>
            _generationResourcesProvider.ChunkGenerationSettings.WallParts;

        protected override int MaxPoolSize => 60;

        public SequentialWallPool(
            IInstantiator container,
            IGenerationResourcesProvider generationResourcesProvider
        ) : base(container)
        {
            _generationResourcesProvider = generationResourcesProvider;

            CreateSubPools();
        }

        public ChunkWallView SpawnWall(int obstacleID)
        {
            return SpawnObject(obstacleID);
        }

        public void DespawnWall(int obstacleID, ChunkWallView wall)
        {
            DespawnObject(obstacleID, wall);
        }
    }
}