#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using System;
using Db.PlayerInput;
using Ecs.Utils;
using Game.Services.DebugService;
using Game.Services.GameState;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Services.PlayerInput
{
    public class KeyboardInputService : IInitializable, IDisposable
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly IDebugService _debugService;
        private readonly GameContext _game;
        private readonly IGameStateService _gameStateService;
        private readonly IKeyboardInputParameters _parameters;

        private IDisposable _inputDisposable;

        public KeyboardInputService(
            ICommandBuffer commandBuffer,
            IKeyboardInputParameters parameters,
            GameContext game,
            IGameStateService gameStateService,
            IDebugService debugService
        )
        {
            _commandBuffer = commandBuffer;
            _parameters = parameters;
            _game = game;
            _gameStateService = gameStateService;
            _debugService = debugService;
        }

        public void Dispose()
        {
            _inputDisposable.Dispose();
        }

        public void Initialize()
        {
            _inputDisposable = Observable.EveryUpdate()
                .Subscribe(_ => Update());
        }

        private void Update()
        {
            if (Input.GetKey(_parameters.FreezeThread))
                _debugService.Freeze();
            
            if (Input.GetKey(_parameters.KillPlayer))
                _commandBuffer.KillPlayer(_game.PlayerEntity.Uid.Value, EDeathReason.Obstacle, false);

            if (Input.GetKeyDown(_parameters.RestartLevel))
                _gameStateService.Restart(true);

            if (!_parameters.IsEnabled)
                return;

            var player = _game.PlayerEntity;

            if (!player.IsPlayerControl)
                return;

            if (player.IsDash)
                return;

            if (Input.GetKeyDown(_parameters.DashActionKey))
                _commandBuffer.Dash(true);

            if (Input.GetKeyDown(_parameters.JumpActionKey))
                _commandBuffer.Swipe(Vector2.up);

            if (Input.GetKeyDown(_parameters.SlideActionKey))
                _commandBuffer.Swipe(Vector2.down);

            if (Input.GetKey(_parameters.MoveLeftActionKey))
                _commandBuffer.MoveHorizontal(-_parameters.HorizontalMoveValue);

            if (Input.GetKey(_parameters.MoveRightActionKey))
                _commandBuffer.MoveHorizontal(_parameters.HorizontalMoveValue);
        }
    }
}
#endif