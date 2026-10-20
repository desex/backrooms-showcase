using System.Collections.Generic;
using Db.Tutorial;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.Hint;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.Analytics;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Low, 510, nameof(EFeatures.Tutorial))]
    public class FinishComebackTutorialSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeScaleService _timeScaleService;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;
        private readonly ITutorialHintService _tutorialHintService;
        private readonly ITutorialParameters _tutorialParameters;
        private readonly ITutorialService _tutorialService;

        public FinishComebackTutorialSystem(
            ITutorialService tutorialService,
            ITutorialHintService tutorialHintService,
            ITimeScaleService timeScaleService,
            ITutorialParameters tutorialParameters,
            ITutorialAnalyticsService tutorialAnalyticsService,
            GameContext game
        ) : base(game)
        {
            _tutorialService = tutorialService;
            _tutorialHintService = tutorialHintService;
            _timeScaleService = timeScaleService;
            _tutorialParameters = tutorialParameters;
            _tutorialAnalyticsService = tutorialAnalyticsService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ComebackInHole);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer
                   && !entity.IsDestroyed
                   && !entity.IsDead
                   && !_tutorialService.TutorialStatesConditions[ETutorialState.Comeback];
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.IsPlayer)
                    continue;

                _tutorialService.FinishTutorial(ETutorialState.Comeback);
                DisableTip();

                _tutorialAnalyticsService.HandleTutorialStateEnd(EAnalyticsTutorialState.Comeback);
            }
        }

        private void DisableTip()
        {
            _tutorialHintService.HideHint(ETutorialHint.Comeback);

            _timeScaleService.ResetTimeScaleSmoothly(
                _tutorialParameters.RevertSlowdownDuration,
                _tutorialParameters.RevertSlowdownEase
            );
        }
    }
}