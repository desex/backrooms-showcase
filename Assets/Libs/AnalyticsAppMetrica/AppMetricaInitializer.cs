using System.Collections.Generic;
using Core;
using Core.Utils;
using Game.Services.Start;
using Zenject;

namespace Libs.AnalyticsAppMetrica
{
    public class AppMetricaInitializer : IInitializable
    {
        private readonly AppMetrica _appMetrica;
        private readonly IYandexAppMetrica _yandexAppMetrica;
        private readonly IGameStartService _gameStartService;
        private readonly ISessionService _sessionService;
        private readonly IAnalyticSignalTypeGetter _analyticSignalTypeGetter;

        public AppMetricaInitializer(
            AppMetrica appMetrica,
            IYandexAppMetrica yandexAppMetrica,
            IGameStartService gameStartService,
            ISessionService sessionService,
            IAnalyticSignalTypeGetter analyticSignalTypeGetter
        )
        {
            _appMetrica = appMetrica;
            _yandexAppMetrica = yandexAppMetrica;
            _gameStartService = gameStartService;
            _sessionService = sessionService;
            _analyticSignalTypeGetter = analyticSignalTypeGetter;
        }

        public void Initialize()
        {
            _appMetrica.AddHandler(new AppMetricaEventHandler(_yandexAppMetrica,
                                                              SendEventsBuffer,
                                                              _gameStartService.DaysSinceReg,
                                                              _sessionService,
                                                              _gameStartService.SessionNumber
                                   )
            ).AddHandler(new AppMetricaAdRevenueHandler(_yandexAppMetrica));

            var signalTypes = _analyticSignalTypeGetter.GetSignalTypes();
            _appMetrica.Init(signalTypes);
        }

        private static List<string> SendEventsBuffer => 
            new List<string>
            {
                "level_start",
                "level_finish",
            };
    }
}