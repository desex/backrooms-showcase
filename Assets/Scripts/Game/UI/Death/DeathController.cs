using System;
using Cysharp.Threading.Tasks;
using Db.Advertisement;
using Db.Fade;
using Db.Fade.Impl;
using Db.MainMenu;
using Db.SoundFxBase;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
using Game.Services.DebugService;
#endif
using Game.Services.GameState;
using Game.Services.Render;
using Game.Services.Sound;
using Game.UI.SettingsPopup;
using Game.Utils.UniTaskExtensions;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using Plugins.SimpleAdWrapper;
using Project.Services.Advertisement;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Game.UI.Death
{
    public class DeathController : UiController<DeathView>,
        IUiInitializable
    {
        private readonly ISoundService _soundService;
        private readonly SignalBus _signalBus;
        private readonly IGameStateService _gameStateService;
        private readonly IFadeParameters _fadeParametersBase;
        private readonly IMainMenuAnimationParameters _monitorAnimationParameters;
        private readonly ICommandBuffer _commandBuffer;
        private readonly ISpeedEffectService _speedEffectService;
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly IInterstitialAvailableService _interstitialAvailableService;
        private readonly IAdInterstitialParameters _adInterstitialParameters;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
        private readonly IDebugService _debugService;
#endif
        private readonly GameContext _game;

        public DeathController(
            ISoundService soundService,
            SignalBus signalBus,
            IGameStateService gameStateService, 
            IFadeParameters fadeParametersBase, 
            IMainMenuAnimationParameters monitorAnimationParameters,
            ICommandBuffer commandBuffer,
            ISpeedEffectService speedEffectService,
            IInterstitialAdService interstitialAdService, 
            IInterstitialAvailableService interstitialAvailableService,
            IAdInterstitialParameters adInterstitialParameters,
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            IDebugService debugService,
#endif
            GameContext game
        )
        {
            _soundService = soundService;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
            _fadeParametersBase = fadeParametersBase;
            _monitorAnimationParameters = monitorAnimationParameters;
            _commandBuffer = commandBuffer;
            _speedEffectService = speedEffectService;
            _interstitialAdService = interstitialAdService;
            _interstitialAvailableService = interstitialAvailableService;
            _adInterstitialParameters = adInterstitialParameters;
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            _debugService = debugService;
#endif
            _game = game;
        }
        
        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        public override void OnHasFocus(bool inFocus)
        {
            // Более показать не могу
        }

        private void OnSettingsButtonClick()
        {
            // Более показать не могу
        }

        private void OnRestart()
        {
            // Более показать не могу
        }

        private async UniTask RestartHandle()
        {
            try
            {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
                if (_debugService.IsAdEnable)
                {
#endif
                    if (_interstitialAvailableService.IsCanShowInterstitial && _adInterstitialParameters.ShouldShowInterstitial)
                    {
                        if (_interstitialAdService.IsLoaded())
                        {
                            await _interstitialAdService.ShowAdAsync("Restart");
                        }

                        await _interstitialAdService.TryPreloadAsync();
                    }
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
                }
#endif
            }
            catch (Exception e)
            {
                Debug.LogError($"[{nameof(DeathController)}]: Something wrong on restart handle - {e.Message}");
            }
            finally
            {
                _gameStateService.Restart(true);
            }
        }
        
        private void DisableSpeedEffect()
        {
            // Более показать не могу
        }
    }
}