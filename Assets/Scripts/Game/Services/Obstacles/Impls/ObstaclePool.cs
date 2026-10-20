using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Services.Providers;
using Game.Utils.Generation;
using Game.Utils.Obstacles;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Services.Obstacles.Impls
{
    public sealed class ObstaclePool : AGenerationPool<EObstacleType, ObstacleGroupView>, IObstaclePool
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;

        protected override IReadOnlyDictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>> PoolObjectVariants =>
            _generationResourcesProvider.ObstaclesBase.Obstacles;

        protected override int MaxPoolSize => 25;

        public ObstaclePool(
            IInstantiator container,
            IRandomProvider randomProvider,
            IGenerationResourcesProvider generationResourcesProvider
        ) : base(container, randomProvider)
        {
            _generationResourcesProvider = generationResourcesProvider;

            CreateSubPools();
        }

        public (ObstacleGroupView obstacle, int randomIndex) SpawnObstacle(EObstacleType obstacleType)
        {
            var (obstacle, randomIndex) = SpawnObject(obstacleType);
            obstacle.ObstacleType = obstacleType;
            return (obstacle, randomIndex);
        }

        public void DespawnObstacle(EObstacleType obstacleType, ObstacleGroupView obstacle, int randomIndex)
        {
            DespawnObject(obstacleType, obstacle, randomIndex);
        }
    }
}