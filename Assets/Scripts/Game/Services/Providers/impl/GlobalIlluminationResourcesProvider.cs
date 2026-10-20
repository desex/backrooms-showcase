using Db.GameLevelsSettings;
using Db.Lights;

namespace Game.Services.Providers.impl
{
    public class GlobalIlluminationResourcesProvider
        : IGlobalIlluminationResourcesProvider, ILevelResourcesProviderSetter
    {
        public ILightSettings GlobalIlluminationSettings { get; private set; }

        public void Set(IGameLevelResources resources)
        {
            // Более показать не могу
        }
    }
}