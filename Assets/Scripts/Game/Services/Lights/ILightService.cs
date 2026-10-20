using Game.Services.Lights.Impl.Builders;
using Game.Utils.Lights;

namespace Game.Services.Lights
{
    public interface ILightService
    {
        void Start(ELightEvent lightEvent);
        void Stop();
        ILightEventBuilder Builder();
    }
}