using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Db.Analytics;
using Game.Services.Tutorial;
using Game.Signals;
using Game.Utils.Analytics;
using Game.Utils.Tutorial;
using Game.Utils.UniTaskExtensions;
using Zenject;

namespace Game.Services.Analytics.Impl
{
    public class TutorialAnalyticsService : ITutorialAnalyticsService
    {
        private const string TUTORIAL_STEP_PREFIX = "tutorial_step_";
        private readonly IAnalyticsParameters _analyticsParameters;
        private readonly Stack<EAnalyticsTutorialState> _currentStates = new Stack<EAnalyticsTutorialState>();
        private readonly GameContext _game;

        private readonly Dictionary<EAnalyticsTutorialState, int> _restartsByState =
            new Dictionary<EAnalyticsTutorialState, int>();

        private readonly ISessionService _sessionService;

        private readonly SignalBus _signalBus;
        private readonly ITutorialService _tutorialService;
        private int? _totalPlayTimeSecOnTutorialStart;

        private int _totalRestartsCount;

        public TutorialAnalyticsService(
            SignalBus signalBus,
            IAnalyticsParameters analyticsParameters,
            ITutorialService tutorialService,
            GameContext game,
            ISessionService sessionService
        )
        {
            _signalBus = signalBus;
            _analyticsParameters = analyticsParameters;
            _tutorialService = tutorialService;
            _game = game;
            _sessionService = sessionService;
        }

        public void HandleTutorialStateStart(EAnalyticsTutorialState state)
        {
            // Более показать не могу
        }

        public void HandleTutorialStateEnd(EAnalyticsTutorialState state)
        {
            // Более показать не могу
        }

        public void HandleTutorialStateRestart()
        {
            // Более показать не могу
        }

        public void CancelTutorialStateAfterRevive()
        {
            // Более показать не могу
        }

        public void HandleTutorialsFinish()
        {
            // Более показать не могу
        }

        private string GetNameForAnalytics(EAnalyticsTutorialState state)
        {
            // Более показать не могу
            return default;
        }
    }
}