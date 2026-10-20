using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
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
    public class TurnTutorialState : ATutorialState
    {
        private int _completedTurnsCount;
        private bool _shouldShowHint = true;
        private IDisposable _turnSubscription;
        private bool _isTurnInProgress;
        private CancellationTokenSource _turnCancellationTokenSource;
        
        public TurnTutorialState(
            ITutorialParameters parameters,
            GameContext game,
            ITimeScaleService timeScaleService,
            ICommandBuffer commandBuffer,
            ETutorialState stateType,
            ITutorialHintService tutorialHintService,
            IPlayerMovementTypeService playerMovementTypeService,
            ITutorialService tutorialService
        ) : base(
            parameters,
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
        
        private async UniTaskVoid StartTurnPresentation(GameEntity player, bool isLeftTurn)
        {
            // Более показать не могу
        }
        
        private async UniTask ExecuteTurnPresentation(GameEntity player, bool isLeftTurn)
        {
            // Более показать не могу
        }
        
        private void HandleTurnCompletion(GameEntity player, bool isLeftTurn, ETutorialHint currentHint, int totalTurnsRequired)
        {
            // Более показать не могу
        }
    }
}