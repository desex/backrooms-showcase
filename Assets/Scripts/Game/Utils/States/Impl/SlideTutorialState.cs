using System;
using Db.Tutorial;
using Db.Tutorial.StateVos;
using Ecs.Utils.ObstaclePass;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.ColorsUtils;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using JCMG.EntitasRedux.Commands;
using R3;

namespace Game.Utils.States.Impl
{
    public class SlideTutorialState : ATutorialState
    {
        private int _presentedMovementsCount;
        private IDisposable _slidePresentationDisposable;

        private bool NeedAffectOnTime => _presentedMovementsCount < SlidingParameters.CountOfSlidesWithSlowdown;
        private TutorialSlidingParameters SlidingParameters => Parameters.SlidingParameters;

        public SlideTutorialState(
            ITutorialParameters parameters,
            GameContext game,
            ITimeScaleService timeScaleService,
            ICommandBuffer commandBuffer,
            ETutorialState stateType,
            ITutorialHintService tutorialHintService,
            IPlayerMovementTypeService playerMovementTypeService,
            ITutorialService tutorialService
        ) : base(parameters,
                 game,
                 timeScaleService,
                 commandBuffer,
                 stateType,
                 tutorialHintService,
                 playerMovementTypeService,
                 tutorialService
        )
        {
        }

        protected override void OnEnter()
        {
            // Более показать не могу
        }

        protected override void OnRestart()
        {
            // Более показать не могу
        }

        protected override void OnExit()
        {
            // Более показать не могу
        }

        private void PrepareForNextSlidingPresentation()
        {
            // Более показать не могу
        }

        private void PresentSliding(GameEntity player, ReadyToObstaclePassData value)
        {
            // Более показать не могу
        }

        private void FinishSlidingPresentation()
        {
            // Более показать не могу
        }
    }
}