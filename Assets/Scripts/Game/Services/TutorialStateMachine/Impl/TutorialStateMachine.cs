using System;
using System.Collections.Generic;
using Db.Tutorial;
using Game.Utils.States;
using Game.Utils.States.Impl;
using Game.Utils.Tutorial;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using R3;
using Zenject;

namespace Game.Services.TutorialStateMachine.Impl
{
    public class TutorialStateMachine : ITutorialStateMachine, IDisposable
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;

        private readonly IInstantiator _instantiator;
        private readonly Queue<IState> _states = new Queue<IState>();
        private readonly ITutorialParameters _tutorialParameters;

        private IDisposable _stateEndDisposable;

        public TutorialStateMachine(
            IInstantiator instantiator,
            ITutorialParameters tutorialParameters,
            GameContext game,
            ICommandBuffer commandBuffer
        )
        {
            _instantiator = instantiator;
            _tutorialParameters = tutorialParameters;
            _game = game;
            _commandBuffer = commandBuffer;
        }

        public void Dispose()
        {
            _stateEndDisposable?.Dispose();

            foreach (var state in _states)
                state.Dispose();
        }

        public void Initialize()
        {
            InitializeStates();
        }

        public void Start()
        {
            SwitchState(Unit.Default);
        }

        private void InitializeStates()
        {
            var states = new Dictionary<ETutorialState, IState>
            {
                {
                    ETutorialState.Tilting, CreateState<TiltingTutorialState>(ETutorialState.Tilting)
                },
                {
                    ETutorialState.Turn, CreateState<TurnTutorialState>(ETutorialState.Turn)
                },
                {
                    ETutorialState.Slide, CreateState<SlideTutorialState>(ETutorialState.Slide)
                },
                {
                    ETutorialState.Jump, CreateState<JumpTutorialState>(ETutorialState.Jump)
                },
                {
                    ETutorialState.DoubleJump, CreateState<DoubleJumpTutorialState>(ETutorialState.DoubleJump)
                },
                {
                    ETutorialState.Final, CreateState<FinalTutorialState>(ETutorialState.Final)
                },
                {
                    ETutorialState.Dash, CreateState<DashTutorialState>(ETutorialState.Dash)
                },
            };

            foreach (var tutorialState in _tutorialParameters.StatesInOrder)
            {
                var currentTutorialState = states[tutorialState];
                _states.Enqueue(currentTutorialState);
            }
        }

        private void SwitchState(Unit unit)
        {
            _stateEndDisposable?.Dispose();

            if (_states.TryDequeue(out var nextState))
                EnterState(nextState);
            else
                FinishTutorial();
        }

        private void FinishTutorial()
        {
            _game.TutorialEntity.ReplaceTutorialState(ETutorialState.None);
            _commandBuffer.FinishTutorial();
        }

        private void EnterState(IState targetState)
        {
            _stateEndDisposable = targetState.StateEnded.Subscribe(_ => SwitchState(Unit.Default));
            _game.TutorialEntity.ReplaceTutorialState(targetState.StateType);
            targetState.Enter();
        }

        private IState CreateState<TState>(ETutorialState tutorialState) where TState : IState
        {
            return _instantiator.Instantiate<TState>(
                new List<object>
                {
                    tutorialState,
                }
            );
        }
    }
}