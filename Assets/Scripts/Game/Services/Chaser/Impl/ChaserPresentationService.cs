using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.Monsters;
using Db.Player;
using Db.SoundFxBase;
using Ecs.Utils;
using Game.Services.Player;
using Game.Services.Sound;
using Game.UI;
using Game.Utils.Movement;
using Game.Utils.MovementBlock;
using Game.Utils.Tutorial;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using SimpleUi.Signals;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Services.Chaser.Impl
{
    public class ChaserPresentationService : IChaserPresentationService, IUiInitializable, IDisposable
    {
        private readonly IPlayerBasicMovementParametersBase _basicMovementParametersBase;
        private readonly IChaserPresentationParameters _chaserPresentationParameters;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerSpeedService _playerSpeedService;
        private readonly SignalBus _signalBus;
        private readonly ISoundService _soundService;
        private readonly ReactiveCommand _startAnimationCommand = new ReactiveCommand();
        private readonly ReactiveCommand _stopAnimationCommand = new ReactiveCommand();
        private readonly CancellationTokenSource _token = new CancellationTokenSource();

        private IDisposable _currentChunkDisposable;
        private bool _isExecuted;
        private IDisposable _tutorialEndDisposable;

        public ChaserPresentationService(
            IPlayerBasicMovementParametersBase basicMovementParametersBase,
            IPlayerSpeedService playerSpeedService,
            ISoundService soundService,
            IChaserPresentationParameters chaserPresentationParameters,
            IPlayerMovementTypeService playerMovementTypeService,
            ICommandBuffer commandBuffer,
            SignalBus signalBus,
            GameContext game
        )
        {
            _basicMovementParametersBase = basicMovementParametersBase;
            _playerSpeedService = playerSpeedService;
            _soundService = soundService;
            _game = game;
            _chaserPresentationParameters = chaserPresentationParameters;
            _playerMovementTypeService = playerMovementTypeService;
            _commandBuffer = commandBuffer;
            _signalBus = signalBus;
        }

        public Observable<Unit> StartAnimationCommand => _startAnimationCommand;
        public Observable<Unit> StopAnimationCommand => _stopAnimationCommand;

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnGameState(GameEntity entity, EGameState value)
        {
            // Более показать не могу
        }

        private void OnInTutorialRemoved(GameEntity entity)
        {
            // Более показать не могу
        }

        private async void StartPresentation(float presentationDelay)
        {
            // Более показать не могу
        }

        private async UniTask StartPresentationAnimation(CancellationToken token)
        {
            // Более показать не могу
        }

        private void StartMoveToPlayer()
        {
            // Более показать не могу
        }

        private void PlayStartSounds()
        {
            // Более показать не могу
        }

        private void PlayPresentationSounds()
        {
            // Более показать не могу
        }

        private void StopPresentation()
        {
            // Более показать не могу
        }

        private bool IsPaused()
        {
            // Более показать не могу
            return default;
        }

        private static void ChangeMovementsState(bool isLocked, GameEntity player)
        {
            // Более показать не могу
        }
    }
}