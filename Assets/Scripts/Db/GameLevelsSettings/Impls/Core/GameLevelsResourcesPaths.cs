using System.Collections.Generic;
using Game.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Core
{
    [CreateAssetMenu(menuName = "Settings/GameLevelsSettings/" + nameof(GameLevelsResourcesPaths), fileName = nameof(GameLevelsResourcesPaths))] //TODO : название самого класса SO
    public class GameLevelsResourcesPaths : SerializedScriptableObject, IGameLevelsResourcesPaths
    {
        [Title("Start Game Level")]
        [field: SerializeField] public EGameLevelType StartGameLevel { get; private set; } = EGameLevelType.Yellow;

#if UNITY_EDITOR
        [Space]
        [Title("Game Levels Resources")]
        [OdinSerialize] private Dictionary<EGameLevelType, GameLevelResources> _gameLevelsResources;
#endif
        
        [HideInInspector]
        [OdinSerialize] private Dictionary<EGameLevelType, string> _gameLevelsResourcesPaths;
        
        public string GetResourcesPath(EGameLevelType levelType)
        {
            if (_gameLevelsResourcesPaths.TryGetValue(levelType, out var path))
                return path;
            
            throw new KeyNotFoundException($"No resources found for levels with type <{levelType} level>");
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _gameLevelsResourcesPaths = new();
            
            foreach (var gameResourcesPath in _gameLevelsResources)
            {
                _gameLevelsResourcesPaths[gameResourcesPath.Key] = gameResourcesPath.Value.name;
            }
        }
#endif
    }
}