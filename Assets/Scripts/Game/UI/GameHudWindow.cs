using Game.UI.ChaseProgressBar;
using Game.UI.Checkpoints.Impl;
using Game.UI.Collectables;
using Game.UI.Combo;
using Game.UI.Death;
using Game.UI.Effects;
using Game.UI.GameRestartButton;
using Game.UI.Pause;
using Game.Ui.PlayerMovementInput;
using Game.UI.Score;
using Game.UI.Tutorial;
using SimpleUi;

namespace Game.UI
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => "GameHud";

        protected override void AddControllers()
        {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
            AddController<DebugController>();
#endif
            
            AddController<UiEffectsController>();
            AddController<PauseController>();
            AddController<PlayerMovementInputController>();
            AddController<DeathBlackoutController>();
            AddController<ComboController>();
            AddController<ScoreController>();
            AddController<CollectablesController>();
            AddController<ChaserController>();
            AddController<CheckpointController>();
            AddController<TutorialHintsController>();
        }
    }
}