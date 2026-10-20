using System.Collections.Generic;
using Core;
using Db.Analytics;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.DistanceCalculation;
using Game.Services.Fps;
using Game.Services.Score;
using Game.Signals;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Game.Utils.Lights;
using Game.Utils.Obstacles;
using UnityEngine;
using Zenject;

namespace Game.Services.Analytics.Impl
{
    public class AnalyticsService : IAnalyticsService
    {
        private const float BEGINNING_CHUNK_RATIO_THRESHOLD = 0.33f;
        private const float CENTER_CHUNK_RATIO_THRESHOLD = 0.66f;

        private readonly List<EScreamerType> _activeScreamers = new List<EScreamerType>();
        private readonly IAnalyticsParameters _analyticsParameters;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ILevelSessionService _levelSessionService;
        private readonly IScoreService _scoreService;
        private readonly ISessionService _sessionService;

        private readonly IAverageFpsService _averageFpsService;
        private readonly SignalBus _signalBus;

        private Uid _currentChunkUid;
        private string _lastDamageReason;
        private ELightEvent _lightEvent = ELightEvent.None;
        private int _reviveCount;

        public AnalyticsService(
            IAverageFpsService averageFpsService,
            SignalBus signalBus,
            GameContext game,
            IScoreService scoreService,
            ISessionService sessionService,
            ILevelSessionService levelSessionService,
            IGameGroupUtils gameGroupUtils,
            IDistanceCalculationService distanceCalculationService,
            IAnalyticsParameters analyticsParameters,
            IChunkSegmentService chunkSegmentService
        )
        {
            _averageFpsService = averageFpsService;
            _signalBus = signalBus;
            _game = game;
            _scoreService = scoreService;
            _sessionService = sessionService;
            _levelSessionService = levelSessionService;
            _gameGroupUtils = gameGroupUtils;
            _distanceCalculationService = distanceCalculationService;
            _analyticsParameters = analyticsParameters;
            _chunkSegmentService = chunkSegmentService;
        }

        public void HandleLevelStart()
        {
            // Более показать не могу
        }

        public void HandleLevelRestart()
        {
            // Более показать не могу
        }

        public void HandleRevive()
        {
            // Более показать не могу
        }

        public void HandleLevelFinish()
        {
            // Более показать не могу
        }

        public void HandlePlayerDeath(EDeathReason deathReason)
        {
            // Более показать не могу
        }

        public void SetCurrentChunk(Uid chunkUid)
        {
            // Более показать не могу
        }

        public void HandleObstacleHit(Uid hitEntityUid)
        {
            // Более показать не могу
        }

        public void SetLightEvent(ELightEvent lightEvent)
        {
            // Более показать не могу
        }

        public void HandleScreamerStart(EScreamerType screamerType)
        {
            // Более показать не могу
        }

        public void HandleScreamerEnd(EScreamerType screamerType)
        {
            // Более показать не могу
        }

        public void HandleLevelTransition(EGameLevelType levelType)
        {
            // Более показать не могу
        }

        private string GetPlayerPositionInChunk(GameEntity chunk)
        {
            // Более показать не могу
            return default;
        }

        private string GetPreviousObstacle(GameEntity player)
        {
            // Более показать не могу
            return default;
        }
    }
}