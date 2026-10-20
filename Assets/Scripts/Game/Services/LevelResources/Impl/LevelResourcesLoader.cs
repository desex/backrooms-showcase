using System.Collections.Generic;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls;
using Db.GameLevelsSettings.Impls.Core;
using Game.Services.Providers;
using Game.Utils;
using UnityEngine;
using Zenject;

namespace Game.Services.LevelResources.Impl
{
    public class LevelResourcesLoader : ILevelResourcesLoader, IInitializable
    {
        private readonly IGameLevelsResourcesPaths _gameLevelsResourcesPaths;
        private readonly List<ILevelResourcesProviderSetter> _resourcesProvidersSetters;

        public LevelResourcesLoader(
            List<ILevelResourcesProviderSetter> resourcesProvidersSetters,
            IGameLevelsResourcesPaths gameLevelsResourcesPaths
        )
        {
            _resourcesProvidersSetters = resourcesProvidersSetters;
            _gameLevelsResourcesPaths = gameLevelsResourcesPaths;
        }

        public void Initialize()
        {
            Load(_gameLevelsResourcesPaths.StartGameLevel);
        }

        public EGameLevelType CurrentLevelType { get; private set; }

        public void Load(EGameLevelType levelType)
        {
            if (CurrentLevelType == levelType)
                return;

            var resources = LoadLevelResources(levelType);

            foreach (var providerSetter in _resourcesProvidersSetters)
                providerSetter.Set(resources);

            CurrentLevelType = levelType;
        }

        private IGameLevelResources LoadLevelResources(EGameLevelType levelType)
        {
            var path = _gameLevelsResourcesPaths.GetResourcesPath(levelType);
            var resources = Resources.Load<GameLevelResources>(path);

            return resources;
        }
    }
}