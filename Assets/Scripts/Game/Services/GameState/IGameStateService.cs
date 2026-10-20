namespace Game.Services.GameState
{
    public interface IGameStateService
    {
        void EnterMainMenu();
        void EnterGameplay();
        void EnterLose();
        void Restart(bool needSkipMainMenu);
        void Revive();
        void EnterRevivingChoice();
        void EnterRevivingChoiceWithDelay();
        void EnterTutorial();
    }
}