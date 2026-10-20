using Db.Lights;

namespace Game.Services.Providers
{
    public interface IGlobalIlluminationResourcesProvider
    {
        ILightSettings GlobalIlluminationSettings { get; }
    }
}