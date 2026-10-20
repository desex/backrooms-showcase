using System;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
using Ecs.Managers;
using Game.Services.Hint;
using Game.Services.Player;
using Game.Services.TimeScale;
using Game.Services.Tutorial;
using Game.Utils.MovementBlock;
using Game.Utils.States;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using R3;

namespace Game.Utils.Tutorial
{
    public abstract class ATutorialState : IState
    {
        protected readonly ITutorialParameters Parameters;
        protected readonly GameContext Game;

        protected readonly CompositeDisposable CommonDisposable = new();
        protected readonly CompositeDisposable ActionDisposable = new();
        
        private readonly ITutorialHintService _tutorialHintService;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ITutorialService _tutorialService;
        private readonly ITimeScaleService _timeScaleService;
        private readonly ICommandBuffer _commandBuffer;
        
        private readonly ReactiveCommand _stateEnded = new();
        
        private IDisposable _afkFailDisposable;
        private EBlockedMovement? _blockedMovementsOnFirstEnter;

        public Observable<Unit> StateEnded => _stateEnded;
        public ETutorialState StateType { get; }
        protected EBlockedMovement BlockedMovementsOnFirstEnter => _blockedMovementsOnFirstEnter!.Value;
        
        protected ATutorialState(
            ITutorialParameters parameters,
            GameContext game,
            ITimeScaleService timeScaleService,
            ICommandBuffer commandBuffer,
            ETutorialState stateType, 
            ITutorialHintService tutorialHintService,
            IPlayerMovementTypeService playerMovementTypeService,
            ITutorialService tutorialService
        )
        {
            Parameters = parameters;
            Game = game;
            _timeScaleService = timeScaleService;
            _commandBuffer = commandBuffer;
            StateType = stateType;
            _tutorialHintService = tutorialHintService;
            _playerMovementTypeService = playerMovementTypeService;
            _tutorialService = tutorialService;
        }

        public void Enter()
        {
            // Более показать не могу
        }

        public virtual void Dispose()
        {
            // Более показать не могу
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
        protected abstract void OnRestart();

        protected void StartMovementPresentation(
            EBlockedMovement targetMovement,
            ETutorialHint hint,
            bool needSlowdownTime = true
        )
        {
            // Более показать не могу
        }
        
        protected void StartMovementPresentationTurn(
            EBlockedMovement targetMovement,
            ETutorialHint hint,
            bool needShowHint,
            bool needSlowdownTime = true
        )
        {
            // Более показать не могу
        }
        
        protected void StartMovementPresentation(
            ETutorialHint hint,
            bool needSlowdownTime = true,
            bool needShowHint = true
        )
        {
            // Более показать не могу
        }

        protected void ResetTimeScale()
        {
            // Более показать не могу
        }

        protected void FinishActionPresentation(
            EBlockedMovement targetMovement,
            ETutorialHint hint,
            bool needSlowdownTime = true
        )
        {
            // Более показать не могу
        }
        
        protected void FinishActionPresentation(
            ETutorialHint hint,
            bool needSlowdownTime = true
        )
        {
            // Более показать не могу
        }

        protected void RestartWithTeleport()
        {
            // Более показать не могу
        }

        protected void SetLockedMovements(EBlockedMovement blockedMovements)
            {
            // Более показать не могу
        }

        private async UniTask Restart()
        {
            // Более показать не могу
        }

        private void Exit()
        {
            // Более показать не могу
        }

        private void OnCurrentChunkId(GameEntity entity, Uid value)
        {
            // Более показать не могу
        }

        private void OnDeadRemoved(GameEntity entity) {
            // Более показать не могу
        }

        private void TryFailAfkPlayer()
        {
            // Более показать не могу
        }
    }
}