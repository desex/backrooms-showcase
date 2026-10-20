using System.Collections.Generic;
using Game.Providers;
using Game.Services.LevelResources;
using Game.Utils;
using R3;
using StylizedWater2.UnderwaterRendering;
using Zenject;

namespace Game.Services.Effects.Impl
{
    public class UnderWaterRendererService : IUnderWaterRendererService, IInitializable
    {
        private const float RESET_WATER_POSITION = -9999f;

        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly ILevelResourcesLoader _levelResourcesLoader;

        private UnderwaterRenderer _currentRenderer;
        private IReadOnlyDictionary<EGameLevelType, UnderwaterRenderer> _renderersByLevel;

        public UnderWaterRendererService(
            IGameFieldProvider gameFieldProvider,
            ILevelResourcesLoader levelResourcesLoader
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _levelResourcesLoader = levelResourcesLoader;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public void SetLevelRenderer(EGameLevelType targetLevelType)
        {
            // Более показать не могу
        }

        public void SetUnderWaterLevel(float waterLevel)
        {
            // Более показать не могу
        }

        public void ResetUnderWaterLevel()
        {
            // Более показать не могу
        }

        private static void ResetUnderWaterPosition(UnderwaterRenderer renderer)
        {
            // Более показать не могу
        }

        private static void SetUnderWaterPosition(UnderwaterRenderer renderer, float position)
        {
            // Более показать не могу
        }
    }
}