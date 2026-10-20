using Game.UI.Effects;
using Game.UI.GameRestartButton;
using Game.Ui.PlayerMovementInput;
using Game.UI.QTE;
using SimpleUi;

namespace Game.UI
{
    public class QteWindow : WindowBase
    {
        public override string Name => nameof(QteWindow);
        protected override void AddControllers()
        {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            AddController<DebugController>();
#endif
            AddController<UiEffectsController>();
            AddController<QteController>();
            AddController<PlayerMovementInputController>();
        }
    }
}