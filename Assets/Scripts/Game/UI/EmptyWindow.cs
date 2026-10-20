using Game.UI.Effects;
using Game.UI.GameRestartButton;
using Game.Ui.PlayerMovementInput;
using SimpleUi;

namespace Game.UI
{
    public class EmptyWindow : WindowBase
    {
        public override string Name => nameof(EmptyWindow);
        protected override void AddControllers()
        {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            AddController<DebugController>();
#endif
            AddController<UiEffectsController>();
            AddController<PlayerMovementInputController>();
        }
    }
}