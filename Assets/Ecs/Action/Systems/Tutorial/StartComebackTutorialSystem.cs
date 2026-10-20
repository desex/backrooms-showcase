using System;
using System.Collections.Generic;
using Db.Tutorial;
using Ecs.Utils;
using Game.Services.Analytics;
using Game.Services.Hint;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.Analytics;
using Game.Utils.Movement;
using Game.Utils.Tutorial;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Low, 500, nameof(EFeatures.Tutorial))]
    public class StartComebackTutorialSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeScaleService _timeScaleService;
        private readonly ITutorialAnalyticsService _tutorialAnalyticsService;
        private readonly ITutorialHintService _tutorialHintService;
        private readonly ITutorialParameters _tutorialParameters;
        private readonly ITutorialService _tutorialService;

        private IDisposable _disposable;

        public StartComebackTutorialSystem(
            ITutorialService tutorialService,
            ITutorialParameters tutorialParameters,
            ITimeScaleService timeScaleService,
            ITutorialHintService tutorialHintService,
            ITutorialAnalyticsService tutorialAnalyticsService,
            GameContext game
        ) : base(game)
        {
            _tutorialService = tutorialService;
            _tutorialParameters = tutorialParameters;
            _timeScaleService = timeScaleService;
            _tutorialHintService = tutorialHintService;
            _tutorialAnalyticsService = tutorialAnalyticsService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ReadyToObstaclePass);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.IsPlayer
                   && !entity.IsDestroyed
                   && !entity.IsDead
                   && !entity.IsTutorialFailTeleporting
                   && entity.HasReadyToObstaclePass
                   && entity.BodyMoveType.Value != EBodyMoveType.ComebackInHole
                   && entity.ReadyToObstaclePass.Value.PassType == EObstaclePassType.HoleComeback
                   && !_tutorialService.TutorialStatesConditions[ETutorialState.Comeback];
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.IsPlayer)
                    continue;

                EnableTip();
                
                _tutorialAnalyticsService.HandleTutorialStateStart(EAnalyticsTutorialState.Comeback);
            }
        }

        private void EnableTip()
        {
            _timeScaleService.ChangeTimeScaleSmoothly(
                _tutorialParameters.SlowdownTimeScale,
                _tutorialParameters.SlowdownDuration,
                _tutorialParameters.SlowdownEase
            );

            _tutorialHintService.ShowHint(ETutorialHint.Comeback);
        }
    }
}