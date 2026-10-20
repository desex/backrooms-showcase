using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Obstacles;
using Game.Services.Providers;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using PdUtils.RandomProvider;

namespace Ecs.Game.Systems.Obstacles
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 920, nameof(EFeatures.Move))]
    public class ObstaclesDistanceGroupSystem : ReactiveSystem<GameEntity>
    {
        private readonly IObstacleGenerationService _obstacleGenerationService;
        private readonly IRandomProvider _randomProvider;
        private readonly IGenerationResourcesProvider _resourcesProvider;

        public ObstaclesDistanceGroupSystem(
            GameContext context,
            IObstacleGenerationService obstacleGenerationService,
            IRandomProvider randomProvider,
            IGenerationResourcesProvider resourcesProvider
        ) : base(context)
        {
            _obstacleGenerationService = obstacleGenerationService;
            _randomProvider = randomProvider;
            _resourcesProvider = resourcesProvider;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            // Более показать не могу
            return default;
        }

        protected override bool Filter(GameEntity entity)
        {
            // Более показать не могу
            return default;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            // Более показать не могу
        }
    }
}