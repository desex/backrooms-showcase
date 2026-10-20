using Cysharp.Threading.Tasks;
using Db.ReviveView;
using DG.Tweening;
using Ecs.Utils;
using Ecs.Utils.ItemCollectables;
using Game.Models.Camera;
using Game.Services.Camera;
using Game.Services.CollectablesItem;
using Game.Services.GameState;
using Game.Services.General;
using Game.Services.Render;
using Game.Services.Score;
using Game.Services.TimeScale;
using Game.Utils;
using Game.Utils.Render;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using Plugins.SimpleAdWrapper;
using R3;
using SimpleUi.Abstracts;
using UnityEngine;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Game.Services.DebugService;
#endif

namespace Game.UI.Revive
{
    public class ReviveController : UiController<ReviveView>, IUiInitializable
    {
        private readonly IReviveViewSettings _reviveViewSettings;
        private readonly ITvLinesService _tvLinesService;
        private readonly IScoreService _scoreService;
        private readonly IGameStateService _gameStateService;
        private readonly ICollectiblesStorage _collectiblesStorage;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IRewardVideoAdService _rewardVideoAdService;
        private readonly ICameraService _cameraService;
        private readonly ISlowingDownReviveService _slowingDownReviveService;
        private readonly GameContext _game;
        private readonly IWhiteNoiseService _whiteNoiseService;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        private readonly IDebugService _debugService;
#endif

        private Tween _countTween;

        public ReviveController(
            ITvLinesService tvLinesService,
            IReviveViewSettings reviveViewSettings,
            IScoreService scoreService,
            IGameStateService gameStateService,
            ICollectiblesStorage collectiblesStorage,
            ICommandBuffer commandBuffer,
            GameContext game,
            IWhiteNoiseService whiteNoiseService,
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            IDebugService debugService,
#endif
            ICameraService cameraService,
            ISlowingDownReviveService slowingDownReviveService,
            IRewardVideoAdService rewardVideoAdService
        )
        {
            _reviveViewSettings = reviveViewSettings;
            _tvLinesService = tvLinesService;
            _scoreService = scoreService;
            _gameStateService = gameStateService;
            _collectiblesStorage = collectiblesStorage;
            _commandBuffer = commandBuffer;
            _game = game;
            _whiteNoiseService = whiteNoiseService;
            _rewardVideoAdService = rewardVideoAdService;
            _cameraService = cameraService;
            _slowingDownReviveService = slowingDownReviveService;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            _debugService = debugService;
#endif
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        public override void OnHide()
        {
            // Более показать не могу
        }

        private void OnShowDeathWindow()
        {
            // Более показать не могу
        }

        private async UniTask OnReviveForAd()
        {
            _countTween.Kill();
            _commandBuffer.CancelReviveCameraFocus();

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            if (_debugService.IsAdEnable)
            {
#endif
                var adResult = await _rewardVideoAdService.ShowAdAsync("revive");

                if (!adResult.CanGiveReward())
                {
                    _gameStateService.EnterLose();
                    return;
                }

                _rewardVideoAdService.TryPreloadAsync().ForgetAndWrapExceptions();

#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            }
#endif

            View.ReviveForAdButton.ResetPressState();
            
            ResetComponentsBeforeOnRevive();
            
            _gameStateService.Revive();

            DelayedInvocationService
                .Invoke(
                    () => _collectiblesStorage.AddCollectibleItem(
                        ECollectableType.AlmondWater,
                        _reviveViewSettings.CountBottlesPerAds
                    ),
                    _reviveViewSettings.DurationForAddBottlesAfterAds
                );
        }

        private void OnReviveForWater()
        {
            // Более показать не могу
        }

        private void ResetComponentsBeforeOnRevive()
        {
            // Более показать не могу
        }

        private void OnUpdateButtonRewardAdState(AdStateUpdate state)
        {
            // Более показать не могу
        }
    }
}