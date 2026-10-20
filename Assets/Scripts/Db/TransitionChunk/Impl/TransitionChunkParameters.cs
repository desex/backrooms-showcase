using System.Collections.Generic;
using Game.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.TransitionChunk.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(TransitionChunkParameters), fileName = nameof(TransitionChunkParameters), order = 1)]
    public class TransitionChunkParameters : SerializedScriptableObject, ITransitionChunkParameters
    {
        [field: Min(10f)]
        [field: Tooltip("Distance in meters between each transition chunk")]
        [field: SerializeField]
        public float DistanceBetweenChunks { get; private set; }

        [field: SerializeField] public int ChunkToGenerateAfterTransitionCount { get; private set; } = 3;
        [field: SerializeField] public float UnscaledDistanceToRunBeforeTimeStop { get; private set; } = 4;
        [field: SerializeField] public float SlowdownMovementDuration { get; private set; } = 0.15f;
        [field: SerializeField] public float ApplyDecreasedTimeScaleDuration { get; private set; } = 0.3f;
        [field: SerializeField] public float SlowdownTimeScale { get; private set; } = 0.4f;
        [field: SerializeField] public float TimeToStartAfterLoad { get; private set; } = 0.6f;
        
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Level From", ValueLabel = "Levels To")]
        private Dictionary<EGameLevelType, List<EGameLevelType>> _levelConnections = new();

        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Level", ValueLabel = "Level cubemap")]
        private Dictionary<EGameLevelType, Cubemap> _levelCubemaps = new();

        public IReadOnlyDictionary<EGameLevelType, List<EGameLevelType>> LevelConnections => _levelConnections;
        public IReadOnlyDictionary<EGameLevelType, Cubemap> LevelCubemaps => _levelCubemaps;
    }
}