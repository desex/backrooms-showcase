using System;
using Db.Analytics;
using Ecs.Game.Components.Tutorial;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.Analytics
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 922, nameof(EFeatures.Common))]
    public class TutorialStepsHandlerAnalyticSystem
        : ComponentReplacedReactiveSystem<GameEntity, TutorialStateComponent, ETutorialState>
    {
        private readonly IAnalyticsParameters _analyticsParameters;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;

        public TutorialStepsHandlerAnalyticSystem(
            GameContext context,
            ITutorialAnalyticsService tutorialAnalyticsService,
            IAnalyticsParameters analyticsParameters
        ) : base(context)
        {
            _tutorialAnalyticsService = tutorialAnalyticsService;
            _analyticsParameters = analyticsParameters;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override ETutorialState GetValue(TutorialStateComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, ETutorialState last, ETutorialState next)
        {
            // Более показать не могу
        }
    }
}