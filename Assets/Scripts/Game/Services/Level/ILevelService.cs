using Game.Utils;

namespace Game.Services.Level
{
    public interface ILevelService
    {
        EGameLevelType ChooseLevel();
        void ClearUsedLevels();
    }
}