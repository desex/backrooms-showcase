using Game.Services.Score;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using R3;

namespace Game.UI.Score
{
    public class ScoreController : UiController<ScoreView>, IUiInitializable
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void UpdateScore(int currentScore)
        {
            // Более показать не могу
        }
        
        private void UpdatePassedDistance(int passedDistanceInMeters)
        {
            // Более показать не могу
        }
    }
}