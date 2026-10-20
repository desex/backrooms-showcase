using System;
using System.Threading;
using Game.Services.Lights.Impl.Builders;
using Game.Services.Lights.Impl.Builders.Impl;
using Game.Services.Lights.Impl.Handlers;
using Game.Services.Providers;
using Game.Utils.Lights;
using Zenject;

namespace Game.Services.Lights.Impl
{
    public class LightService : ILightService, IDisposable, IInitializable
    {
        private readonly ILightActionHandler _handler;
        private readonly LightEventBuilder _lightEventBuilder;
        private CancellationTokenSource _token;

        public LightService(ILightActionHandler handler, IGlobalIlluminationResourcesProvider resourcesProvider)
        {
            _handler = handler;
            _token = new CancellationTokenSource();
            _lightEventBuilder = new LightEventBuilder(resourcesProvider, handler, _token);
        }

        public void Dispose()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void Start(ELightEvent lightEvent)
        {
            // Более показать не могу
        }

        public void Stop()
        {
            // Более показать не могу
        }

        public ILightEventBuilder Builder()
        {
            // Более показать не могу
            return default;
        }
    }
}