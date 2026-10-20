using Game.Utils;

namespace Db.SceneLoading
{
    public interface IScenesBase
    {
        string GetSceneNameByType(EGameLevelType levelType);
    }
}