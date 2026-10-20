using Db.ChasePlayer;
using Db.Render.Glitch;
using Game.Services.Chaser;
using Game.Services.Render;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using R3;

namespace Game.UI.ChaseProgressBar
{
    public class ChaserController : UiController<ChaserView>, IUiInitializable
    {
        private readonly IChaserPresentationService _chaserPresentationService;
        private readonly GameContext _game;
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly IGlitchService _glitchService;
        private readonly IGlitchParameters _chaserGlitchEffectParameters;

        public ChaserController(
            IChaserPresentationService chaserPresentationService,
            GameContext game, 
            IChasePlayerParameters chasePlayerParameters,
            IGlitchService glitchService,
            IGlitchParameters chaserGlitchEffectParameters
        )
        {
            _chaserPresentationService = chaserPresentationService;
            _game = game;
            _chasePlayerParameters = chasePlayerParameters;
            _glitchService = glitchService;
            _chaserGlitchEffectParameters = chaserGlitchEffectParameters;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void GlitchEffectEnabled(float barProgress)
        {
            // Более показать не могу
        }
        
        private void Update(GameEntity entity, float value)
        {
            // Более показать не могу
        }
    }
}