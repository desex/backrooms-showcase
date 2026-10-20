using System;
using Game.Utils.Lights;

namespace Game.Services.Lights.Impl.Builders
{
    public interface ILightEventBuilder
    {
        void Build();
        ILightEventBuilder SetEvent(ELightEvent lightEvent);
        ILightEventBuilder SetOnEnd(Action onEnd);
        ILightEventBuilder SetOnStart(Action onStart);
    }
}