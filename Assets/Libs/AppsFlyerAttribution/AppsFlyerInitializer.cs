using Core;
using Core.Utils;
using Game.Services.Start;
using Libs.AppsFlyerAttribution.Analytics;
using Zenject;

namespace Libs.AppsFlyerAttribution
{
    public class AppsFlyerInitializer : IInitializable
    {
        private readonly AppsFlyerAnalytic _appsFlyerAnalytic;
        private readonly IGameStartService _gameStartService;
        private readonly ISessionService _sessionService;
        private readonly IAnalyticSignalTypeGetter _analyticSignalTypeGetter;

        public AppsFlyerInitializer(
            AppsFlyerAnalytic appsFlyerAnalytic,
            IGameStartService gameStartService,
            ISessionService sessionService,
            IAnalyticSignalTypeGetter analyticSignalTypeGetter)
        {
            _appsFlyerAnalytic = appsFlyerAnalytic;
            _gameStartService = gameStartService;
            _sessionService = sessionService;
            _analyticSignalTypeGetter = analyticSignalTypeGetter;
        }

        public void Initialize()
        {
            _appsFlyerAnalytic
                .AddHandler(new AppsFlyerEventHandler(_gameStartService.DaysSinceReg, _sessionService, 
                    _gameStartService.SessionNumber));

            var signalTypes = _analyticSignalTypeGetter.GetSignalTypes();
            _appsFlyerAnalytic.Init(signalTypes);
        }
    }
}