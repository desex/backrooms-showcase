using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Analytics;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Analytics
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 924, nameof(EFeatures.Common))]
    public class TutorialRestartsHandlerAnalyticSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;

        public TutorialRestartsHandlerAnalyticSystem(
            GameContext game,
            ITutorialAnalyticsService tutorialAnalyticsService
        ) : base(game)
        {
            _game = game;
            _tutorialAnalyticsService = tutorialAnalyticsService;
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