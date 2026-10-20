using Db.UI.Checkpoint;
using Game.Services.CheckPoints;
using Game.Services.Score;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using R3;

namespace Game.UI.Checkpoints.Impl
{
    public class CheckpointController : UiController<CheckpointView>, IUiInitializable
    {
        private readonly GameContext _game;
        private readonly IScoreService _scoreService;
        private readonly ICheckpointScoreService _checkpointScoreService;
        private readonly ICheckpointService _checkpointService;
        private readonly ICheckpointViewParameters _parameters;

        public CheckpointController(
            GameContext game,
            IScoreService scoreService,
            ICheckpointScoreService checkpointScoreService,
            ICheckpointService checkpointService,
            ICheckpointViewParameters parameters
        )
        {
            _game = game;
            _scoreService = scoreService;
            _checkpointScoreService = checkpointScoreService;
            _checkpointService = checkpointService;
            _parameters = parameters;
        }
        
        public void Initialize()
        {
            // Более показать не могу
        }

        private void OnShowCheckpointText(GameEntity entity, int value)
        {
            // Более показать не могу
        }
    }
}