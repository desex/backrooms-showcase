using Game.Utils;

namespace Game.Services.LevelResources
{
    public interface ILevelResourcesLoader
    {
        public EGameLevelType CurrentLevelType { get; }
        void Load(EGameLevelType levelType);
    }
}