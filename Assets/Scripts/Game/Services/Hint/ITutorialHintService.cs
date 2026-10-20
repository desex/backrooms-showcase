using Game.Utils.Tutorial;
using R3;

namespace Game.Services.Hint
{
    public interface ITutorialHintService
    {
        Observable<ETutorialHint> NeedHintShow { get; }
        Observable<ETutorialHint> NeedHintHide { get; }
        Observable<Unit> NeedHideAllHints { get; }
        ETutorialHint ActiveHint { get; }
        void ShowHint(ETutorialHint tutorialState);
        void HideHint(ETutorialHint tutorialState);
        void HideAllHints();
    }
}