using Db.GameLevelsSettings;

namespace Game.Services.Providers
{
    public interface ILevelResourcesProviderSetter
    {
        void Set(IGameLevelResources resources);
    }
}