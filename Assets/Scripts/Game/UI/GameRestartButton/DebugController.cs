#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Core.Utils;
using Db.SoundFxBase;
using Ecs.Managers;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.DebugService;
using Game.Services.GameState;
using Game.Services.Player;
using Game.Services.Sound;
using Game.Services.Tutorial;
using Game.Services.UiVisibility;
using Game.Utils.Collisions;
using Game.Utils.Movement;
using PdUtils.Interfaces;
using PdUtils.PlayerPrefs;
using R3;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Game.UI.GameRestartButton
{
    public class DebugController : UiController<DebugView>, IUiInitializable
    {
        private readonly IGameStateService _gameStateService;
        private readonly ISoundService _soundService;
        private readonly IPlayerSlideService _playerSlideService;
        private readonly GameContext _game;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICollisionService _collisionService;
        private readonly IPlayerPrefsManager _playerPrefsManager;
        private readonly IDebugService _debugService;
        private readonly ITutorialService _tutorialService;
        private readonly IUiVisibilityService _uiVisibilityService;

        private bool _isCollisionEnabled = true;
        private bool _isDeathFromChaserEnabled = true;
        private bool _isPlayerMovementEnabled = true;
        private bool _isChaserUiEnabled = true;
        private bool _isOtherUiEnabled = true;

        public DebugController(
            IGameStateService gameStateService,
            ISoundService soundService,
            IPlayerSlideService playerSlideService,
            GameContext game,
            ICollectiblesStorage collectiblesStorage,
            ICollisionService collisionService,
            IPlayerPrefsManager playerPrefsManager,
            IDebugService debugService,
            ITutorialService tutorialService,
            IUiVisibilityService uiVisibilityService
        )
        {
            _gameStateService = gameStateService;
            _soundService = soundService;
            _playerSlideService = playerSlideService;
            _game = game;
            _collectiblesStorage = collectiblesStorage;
            _collisionService = collisionService;
            _playerPrefsManager = playerPrefsManager;
            _debugService = debugService;
            _tutorialService = tutorialService;
            _uiVisibilityService = uiVisibilityService;
        }
        
        public void Initialize()
        {
            // Более показать не могу
        }

        private void ChangeFpsLock()
        {
            // Более показать не могу
        }

        private void UpdateFpsLockText()
        {
            // Более показать не могу
        }

        private void OnCurrentChunkId(GameEntity entity, Uid value)
        {
            // Более показать не могу
        }

        private void OnToggleDeathFromChaser()
        {
            // Более показать не могу
        }

        private void OnTogglePlayerMovement()
        {
            // Более показать не могу
        }

        private void AddCollectable(ECollectableType collectableType, int value)
        {
            // Более показать не могу
        }

        private void ResetCollectable(ECollectableType collectableType)
        {
            // Более показать не могу
        }

        private void UpdateCollectiblesStats()
        {
            // Более показать не могу
        }

        private void OnChangeChaserUiVisibility()
        {
            // Более показать не могу
        }

        private void OnChangeOtherUiVisibility()
        {
            // Более показать не могу
        }

        private void OnToggleButton()
        {
            // Более показать не могу
        }

        private void OnCloseButton()
        {
            // Более показать не могу
        }

        private void OnStatsToggleButton()
        {
            // Более показать не могу
        }

        private void OnDevStatsToggleButton()
        {
            // Более показать не могу
        }

        private void OnRestart(bool needIgnoreTutorial)
        {
            // Более показать не могу
        }

        private void OnChangeSlidesCount()
        {
            // Более показать не могу
        }

        private void TurnPage(bool shouldTurnNext)
        {
            // Более показать не могу
        }

        private void OnToggleCollision()
        {
            // Более показать не могу
        }

        private void OnPlayerSpeed(GameEntity entity, ForwardSpeed value)
        {
            // Более показать не могу
        }

        private void OnChaserSpeed(GameEntity entity, ForwardSpeed value)
        {
            // Более показать не могу
        }

        private void DisableCollisions(bool playerCollisionEnabled)
        {
            // Более показать не могу
        }

        private void ToggleAdsHandler()
        {
            // Более показать не могу
        }
    }
}
#endif