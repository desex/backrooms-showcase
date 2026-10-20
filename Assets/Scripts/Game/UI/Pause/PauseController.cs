using Db.SoundFxBase;
using Game.Services.Sound;
using Game.UI.SettingsPopup;
using PdUtils.Interfaces;
using R3;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using Zenject;

namespace Game.UI.Pause
{
    public class PauseController : UiController<PauseView>, IUiInitializable
    {
        private readonly GameContext _game;
        private readonly SignalBus _signalBus;
        private readonly ISoundService _soundService;

        public PauseController(
            GameContext game,
            SignalBus signalBus,
            ISoundService soundService
        )
        {
            _game = game;
            _signalBus = signalBus;
            _soundService = soundService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnPause()
        {
            // Более показать не могу
        }

        public override void OnHasFocus(bool inFocus)
        {
            // Более показать не могу
        }
    }
}