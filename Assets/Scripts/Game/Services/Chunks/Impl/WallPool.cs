using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.Providers;
using Game.Utils.Chunks;
using Game.Utils.Generation;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Services.Chunks.Impl
{
    public sealed class WallPool : AGenerationPool<EChunkWallType, ChunkWallView>, IWallPool
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        protected override int MaxPoolSize => 50;

        protected override IReadOnlyDictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> PoolObjectVariants =>
            _generationResourcesProvider.ChunkGenerationSettings.WallParts;

        public WallPool(
            IInstantiator container,
            IRandomProvider randomProvider,
            IGenerationResourcesProvider generationResourcesProvider
        ) : base(container, randomProvider)
        {
            _generationResourcesProvider = generationResourcesProvider;

            CreateSubPools();
        }

        public (ChunkWallView wall, int randomIndex) SpawnWall(EChunkWallType wallType)
        {
            return SpawnObject(wallType);
        }

        public void DespawnWall(EChunkWallType wallType, ChunkWallView wall, int randomIndex)
        {
            DespawnObject(wallType, wall, randomIndex);
        }
    }
}