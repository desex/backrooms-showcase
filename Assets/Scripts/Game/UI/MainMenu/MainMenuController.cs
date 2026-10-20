using Db.MainMenu;
using Db.SoundFxBase;
using Db.TvLines;
using Game.Services.GameState;
using Game.Services.General;
using Game.Services.Render;
using Game.Services.Sound;
using Game.Services.Start;
using Game.UI.SettingsPopup;
using Game.Utils.Render;
using PdUtils.Interfaces;
using R3;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using Zenject;

namespace Game.UI.MainMenu
{
    public class MainMenuController : UiController<MainMenuView>, IUiInitializable
    {
        private readonly IGameStateService _gameStateService;
        private readonly ITvLinesService _tvLinesService;
        private readonly ITvLinesParameters _tvLinesParameters;
        private readonly SignalBus _signalBus;
        private readonly IGameStartService _gameStartService;
        private readonly ISoundService _soundService;
        private readonly IMainMenuAnimationParameters _monitorAnimationParameters;
        private readonly GameContext _game;
        private readonly IWhiteNoiseService _whiteNoiseService;

        public MainMenuController(
            IGameStateService gameStateService,
            ITvLinesService tvLinesService,
            ITvLinesParameters tvLinesParameters,
            SignalBus signalBus,
            IGameStartService gameStartService,
            ISoundService soundService,
            IMainMenuAnimationParameters monitorAnimationParameters,
            GameContext game,
            IWhiteNoiseService whiteNoiseService
        )
        {
            _gameStateService = gameStateService;
            _tvLinesService = tvLinesService;
            _tvLinesParameters = tvLinesParameters;
            _signalBus = signalBus;
            _gameStartService = gameStartService;
            _soundService = soundService;
            _monitorAnimationParameters = monitorAnimationParameters;
            _game = game;
            _whiteNoiseService = whiteNoiseService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        private void PlayStartGameAnimation(bool animateCassette = true)
        {
            // Более показать не могу
        }

        private void EnterGameplayWithoutAnimation(bool animateCassette)
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
    }
}