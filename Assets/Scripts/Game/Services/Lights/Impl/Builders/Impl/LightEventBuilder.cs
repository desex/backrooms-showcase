using System;
using System.Threading;
using Game.Services.Lights.Impl.Builders.Data;
using Game.Services.Lights.Impl.Handlers;
using Game.Services.Providers;
using Game.Utils.Lights;

namespace Game.Services.Lights.Impl.Builders.Impl
{
    public class LightEventBuilder : ILightEventBuilder
    {
        private readonly LightEventBuilderData _data;
        private readonly ILightActionHandler _handler;
        private readonly IGlobalIlluminationResourcesProvider _lightResourcesProvider;

        public LightEventBuilder(
            IGlobalIlluminationResourcesProvider lightResourcesProvider,
            ILightActionHandler handler,
            CancellationTokenSource defaultToken
        )
        {
            _data = new LightEventBuilderData(defaultToken);
            _lightResourcesProvider = lightResourcesProvider;
            _handler = handler;
        }

        public async void Build()
        {
            // Более показать не могу
        }

        public ILightEventBuilder SetEvent(ELightEvent lightEvent)
        {
            // Более показать не могу
            return default;
        }

        public ILightEventBuilder SetOnEnd(Action onEnd)
        {
            // Более показать не могу
            return default;
        }

        public ILightEventBuilder SetOnStart(Action onStart)
        {
            // Более показать не могу
            return default;
        }

        public void SetDefaultToken(CancellationTokenSource token)
        {
            // Более показать не могу
        }
    }
}