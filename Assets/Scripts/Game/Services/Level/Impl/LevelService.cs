using System;
using System.Collections.Generic;
using Db.TransitionChunk;
using Game.Services.CurvedWorld;
using Game.Services.LevelResources;
using Game.Utils;
using PdUtils.Interfaces;
using PdUtils.RandomProvider;

namespace Game.Services.Level.Impl
{
    public class LevelService : ILevelService, IUiInitializable
    {
        private static readonly EGameLevelType[] _levels = (EGameLevelType[])Enum.GetValues(typeof(EGameLevelType));

        private readonly Dictionary<EGameLevelType, bool> _availableLevels = new Dictionary<EGameLevelType, bool>();
        private readonly ILevelResourcesLoader _levelResourcesLoader;
        private readonly IRandomProvider _randomProvider;
        private readonly ICurvedWorldService _curvedWorldService;
        private readonly ITransitionChunkParameters _transitionChunkParameters;

        public LevelService(
            ILevelResourcesLoader levelResourcesLoader,
            IRandomProvider randomProvider,
            ITransitionChunkParameters transitionChunkParameters,
            ICurvedWorldService curvedWorldService
        )
        {
            _levelResourcesLoader = levelResourcesLoader;
            _randomProvider = randomProvider;
            _transitionChunkParameters = transitionChunkParameters;
            _curvedWorldService = curvedWorldService;
        }

        public EGameLevelType ChooseLevel()
        {
            // Более показать не могу
            return default;
        }

        public void ClearUsedLevels()
        {
            // Более показать не могу
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void ResetLevelsToDefault()
        {
            // Более показать не могу
        }
    }
}