using Db.Fade;
using Db.Fade.Impl;
using Game.Services.Render;
using Game.Services.Score;
using Game.Utils.Render;
using SimpleUi.Abstracts;

namespace Game.UI.FinalScore
{
    public class FinalScoreController : UiController<FinalScoreView>
    {
        private readonly IScoreService _scoreService;
        private readonly ITvLinesService _tvLinesService;
        private readonly IFadeParameters _fadeParameters;

        public FinalScoreController(
            IScoreService scoreService,
            ITvLinesService tvLinesService,
            IFadeParameters fadeParameters 
        )
        {
            _scoreService = scoreService;
            _tvLinesService = tvLinesService;
            _fadeParameters = fadeParameters;
        }

        public override void OnShow()
        {
            // Более показать не могу
        }

        public override void OnHide()
        {
            // Более показать не могу
        }
    }
}