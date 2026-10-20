using System.Collections.Generic;
using Db.Tutorial;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.Hint;
using Game.Services.TimeScale;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Low, 505, nameof(EFeatures.Tutorial))]
    public class AbortComebackTutorialSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
        private readonly ITimeScaleService _timeScaleService;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;
        private readonly ITutorialHintService _tutorialHintService;
        private readonly ITutorialParameters _tutorialParameters;

        public AbortComebackTutorialSystem(
            GameContext game,
            ITutorialHintService tutorialHintService,
            ITimeScaleService timeScaleService,
            ITutorialParameters tutorialParameters,
            ITutorialAnalyticsService tutorialAnalyticsService
        ) : base(game)
        {
            _tutorialHintService = tutorialHintService;
            _timeScaleService = timeScaleService;
            _tutorialParameters = tutorialParameters;
            _tutorialAnalyticsService = tutorialAnalyticsService;
            _game = game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.FallingInHole.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer && !entity.IsDestroyed && _tutorialHintService.ActiveHint == ETutorialHint.Comeback;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.IsPlayer)
                    continue;

                if (_game.TutorialEntity.IsInTutorial)
                    _tutorialAnalyticsService.CancelTutorialStateAfterRevive();

                DisableTip();
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