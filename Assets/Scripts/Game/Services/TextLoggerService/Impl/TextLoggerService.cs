using System.Globalization;
using Ecs.Utils.ItemCollectables;
using Game.Services.CollectablesItem;
using Game.Services.LevelResources;
using Game.Services.Score;
using UnityEngine;

namespace Game.Services
{
    public class TextLoggerService : ITextLoggerService
    {
        private readonly ICollectiblesStorage _collectibles;
        private readonly ILevelResourcesLoader _levelLoader;
        private readonly IScoreService _scoreService;
        private readonly GameContext _game;

        public TextLoggerService(
            ICollectiblesStorage collectibles,
            ILevelResourcesLoader levelLoader,
            IScoreService scoreService,
            GameContext game
        )
        {
            _collectibles = collectibles;
            _levelLoader = levelLoader;
            _scoreService = scoreService;
            _game = game;
        }

        public string GetLog()
        {
            // Более показать не могу
            return default;
        }

        private string GetSequenceName()
        {
            // Более показать не могу
            return default;
        }

        private string GetPlayerSpeed()
        {
            // Более показать не могу
            return default;
        }

        private string GetChaserSpeed()
        {
            // Более показать не могу
            return default;
        }

        private string GetAlmondWater()
        {
            // Более показать не могу
            return default;
        }

        private string GetCassette()
        {
            // Более показать не могу
            return default;
        }

        private string GetBank()
        {
            // Более показать не могу
            return default;
        }

        private string GetLevel()
        {
            // Более показать не могу
            return default;
        }

        private string GetPassedDistance()
        {
            // Более показать не могу
            return default;
        }

        private string GetPointsEarned()
        {
            // Более показать не могу
            return default;
        }

        private string GetPlayerPosition()
        {
            // Более показать не могу
            return default;
        }

        private string GetTimePassed()
        {
            // Более показать не могу
            return default;
        }
    }
}