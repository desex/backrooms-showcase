using System;
using Db.Tutorial;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.ColorsUtils;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using JCMG.EntitasRedux.Commands;
using R3;

namespace Game.Utils.States.Impl
{
    public class TiltingTutorialState : ATutorialState
    {
        private IDisposable _disposable;

        public TiltingTutorialState(
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

        protected override void OnExit()
        {
            // Более показать не могу
        }

        protected override void OnRestart() { }

        private void FinishFirstPresentationAfterDelay(GameEntity entity, ETiltType tiltType)
        {
            // Более показать не могу
        }


        private void StartSecondTiltingPresentation()
        {
            // Более показать не могу
        }
        
        private void OnTilting(GameEntity entity, ETiltType tiltType)
        {
            // Более показать не могу
        }
    }
}