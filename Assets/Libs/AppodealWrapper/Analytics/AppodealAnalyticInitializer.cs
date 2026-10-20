using Core;
using Core.Utils;
using Game.Services.Start;
using Zenject;

namespace Libs.AppodealWrapper.Analytics
{
    public class AppodealAnalyticInitializer : IInitializable
    {
        private readonly AppodealAnalytic _appodealAnalytic;
        private readonly IGameStartService _gameStartService;
        private readonly ISessionService _sessionService;

        private readonly IAnalyticSignalTypeGetter _analyticSignalTypeGetter;

        // private readonly ILevelService _levelService;

        public AppodealAnalyticInitializer(
            AppodealAnalytic appodealAnalytic,
            IGameStartService gameStartService,
            ISessionService sessionService,
            IAnalyticSignalTypeGetter analyticSignalTypeGetter
            // ILevelService levelService
            )
        {
            _appodealAnalytic = appodealAnalytic;
            _gameStartService = gameStartService;
            _sessionService = sessionService;
            _analyticSignalTypeGetter = analyticSignalTypeGetter;

            // _levelService = levelService;
        }

        public void Initialize()
        {
            _appodealAnalytic
                .AddHandler(new AppodealEventHandler(_gameStartService.DaysSinceReg, _sessionService, 
                    _gameStartService.SessionNumber));

            var signalTypes = _analyticSignalTypeGetter.GetSignalTypes();
            _appodealAnalytic.Init(signalTypes);
        }
    }
}