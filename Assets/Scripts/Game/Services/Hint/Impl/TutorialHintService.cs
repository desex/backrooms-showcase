using Game.Utils.Tutorial;
using R3;

namespace Game.Services.Hint.Impl
{
    public class TutorialHintService : ITutorialHintService
    {
        private readonly ReactiveCommand _needHideAllHints = new ReactiveCommand();
        private readonly ReactiveCommand<ETutorialHint> _needHintHide = new ReactiveCommand<ETutorialHint>();
        private readonly ReactiveCommand<ETutorialHint> _needHintShow = new ReactiveCommand<ETutorialHint>();

        public Observable<ETutorialHint> NeedHintShow => _needHintShow;
        public Observable<ETutorialHint> NeedHintHide => _needHintHide;
        public Observable<Unit> NeedHideAllHints => _needHideAllHints;
        public ETutorialHint ActiveHint { get; private set; }

        public void ShowHint(ETutorialHint tutorialState)
        {
            // Более показать не могу
        }

        public void HideHint(ETutorialHint tutorialState)
        {
            // Более показать не могу
        }

        public void HideAllHints()
        {
            // Более показать не могу
        }
    }
}