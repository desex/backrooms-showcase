using Game.UI.Death;
using Game.UI.Effects;
using Game.UI.GameRestartButton;
using Game.Ui.PlayerMovementInput;
using Game.UI.Tutorial;
using SimpleUi;

namespace Game.UI
{
    public class TutorialWindow : WindowBase
    {
        public override string Name => nameof(TutorialWindow);
        
        protected override void AddControllers()
        {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            AddController<DebugController>();
#endif
            AddController<TutorialHintsController>();
            AddController<UiEffectsController>();
            AddController<PlayerMovementInputController>();
            AddController<DeathBlackoutController>();
        }
    }
}