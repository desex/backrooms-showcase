using Game.Services.Hint;
using Game.Utils.Tutorial;
using SimpleUi.Abstracts;
using Zenject;
using R3;

namespace Game.UI.Tutorial
{
    public class TutorialHintsController : UiController<TutorialHintsView>, IInitializable
    {
        private readonly ITutorialHintService _tutorialHintService;

        public TutorialHintsController(ITutorialHintService tutorialHintService)
        {
            _tutorialHintService = tutorialHintService;
        }
        
        public void Initialize()
        {
            // Более показать не могу
        }

        private void ShowHint(ETutorialHint hint)
        {
            // Более показать не могу
        }
        
        private void HideHint(ETutorialHint hint)
        {
            // Более показать не могу
        }
        
        private void HideAllHints()
        {
            // Более показать не могу
        }
    }
}