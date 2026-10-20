using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Services.Providers;
using Game.Utils.Chunks;
using Game.Utils.Generation;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Services.Chunks.Impl
{
    public sealed class ChunkPool : AGenerationPool<EChunkType, ChunkView>, IChunkPool
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;

        protected override int MaxPoolSize => 20;

        protected override IReadOnlyDictionary<EChunkType, IReadOnlyList<ChunkView>> PoolObjectVariants =>
            _generationResourcesProvider.ChunkGenerationSettings.Chunks;

        public ChunkPool(
            IInstantiator container,
            IRandomProvider randomProvider,
            IGenerationResourcesProvider generationResourcesProvider
        ) : base(container, randomProvider)
        {
            _generationResourcesProvider = generationResourcesProvider;

            CreateSubPools();
        }

        public (ChunkView chunk, int randomIndex) SpawnChunk(EChunkType chunkType)
        {
            return SpawnObject(chunkType);
        }

        public void DespawnChunk(EChunkType chunkType, ChunkView chunk, int randomIndex)
        {
            DespawnObject(chunkType, chunk, randomIndex);
        }
    }
}