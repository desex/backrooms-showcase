using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Services.Providers;
using Game.Utils.Generation;
using Game.Utils.Obstacles;
using Zenject;

namespace Game.Services.Obstacles.Impls
{
    public class SequentialObstaclePool
        : ASequenceGenerationPool<EObstacleType, ObstacleGroupView>, ISequentialObstaclePool
    {
        private readonly IGenerationResourcesProvider _generationResourcesProvider;

        protected override IReadOnlyDictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>> PoolObjectVariants =>
            _generationResourcesProvider.ObstaclesBase.Obstacles;

        protected override int MaxPoolSize => 25;

        public SequentialObstaclePool(
            IInstantiator container,
            IGenerationResourcesProvider generationResourcesProvider
        ) : base(container)
        {
            _generationResourcesProvider = generationResourcesProvider;

            CreateSubPools();
        }

        public ObstacleGroupView SpawnObstacle(EObstacleType obstacleType, int obstacleID)
        {
            var obstacle = SpawnObject(obstacleID);
            obstacle.ObstacleType = obstacleType;
            return obstacle;
        }

        public void DespawnObstacle(int obstacleID, ObstacleGroupView obstacle)
        {
            DespawnObject(obstacleID, obstacle);
        }
    }
}