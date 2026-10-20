using Game.Utils;

namespace Db.GameLevelsSettings.Impls
{
    public interface IGameLevelsResourcesPaths
    {
        EGameLevelType StartGameLevel { get; }
        string GetResourcesPath(EGameLevelType sceneType);
    }
}