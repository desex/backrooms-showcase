using Db.Tutorial;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.ColorsUtils;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using JCMG.EntitasRedux.Commands;

namespace Game.Utils.States.Impl
{
    public class FinalTutorialState : ATutorialState
    {
        public FinalTutorialState(
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
    }
}