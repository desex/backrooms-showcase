using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Views.Linkable;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 700, nameof(EFeatures.Initialization))]
    public class InstantiateSystem : ReactiveSystem<GameEntity>
    {
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly ISpawnService<GameEntity, IObjectLinkable> _spawnService;

        public InstantiateSystem(
            GameContext game,
            ILinkedEntityRepository linkedEntityRepository,
            ISpawnService<GameEntity, IObjectLinkable> spawnService
        ) : base(game)
        {
            _linkedEntityRepository = linkedEntityRepository;
            _spawnService = spawnService;
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