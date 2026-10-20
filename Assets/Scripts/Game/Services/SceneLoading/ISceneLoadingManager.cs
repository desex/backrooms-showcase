namespace Game.Services.SceneLoading
{
    public interface ISceneLoadingManager
    {
        void LoadGameFromSplash();
        void ReloadGame();
        void ReloadGameWithMainMenuSkip();

        float GetProgress();
        void TransitTo(string sceneName);
    }
}