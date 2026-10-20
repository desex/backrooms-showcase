using System.Collections.Generic;
using Ecs.Utils.ItemCollectables;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [CreateAssetMenu(menuName = "Settings/Sequence/" + nameof(SequenceParameters), fileName = nameof(SequenceParameters))]
    public class SequenceParameters : SerializedScriptableObject
    {
        [Space]
        public EChunkType ChunkType;

        [field: Space]
        [field: SerializeField]
        public bool ShouldSpawnCollectables { get; private set; }

        [field: Space]
        [field: ShowIf(nameof(ChunkType), EChunkType.Straight)]
        [field: SerializeField]
        [field: Title("Wall settings:")]
        [field: ListDrawerSettings(ShowIndexLabels = true, HideAddButton = false, HideRemoveButton = false)]
        public List<WallPairTypes> WallsSettings { get; private set; }
       
        [field: Space]
        [field: ShowIf(nameof(ChunkType), EChunkType.Straight)]
        [field: SerializeField]
        [field: Title("Obstacles settings:")]
        [field: ListDrawerSettings(ShowIndexLabels = true)]
        public List<SequenceObstacle> ObstaclesSettings { get; private set; }
        
        [field: Space]
        [field: SerializeField]
        [field: HideIf(nameof(ChunkType), EChunkType.Straight)]
        [field: Title("Unique chunk Obstacle Settings:")]
        [field: ListDrawerSettings(ShowIndexLabels = true, HideAddButton = false, HideRemoveButton = false)]
        public List<UniqueChunkObstacleSettings> UniquechunkObstacleSettings { get; private set; }

        
        [Space]
        [ShowIf("ShouldSpawnCollectables")]
        [ValidateInput("ValidateCollectablesNull")]
        [Title("Collectable spawn settings: ")]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Collectable type", ValueLabel = "Spawn offsets")]
        private Dictionary<ECollectableType, List<Vector3>> _collectablesSpawnInfo;
        
        public IReadOnlyDictionary<ECollectableType, List<Vector3>> CollectablesSpawnInfo => _collectablesSpawnInfo;

        [Space]
        [SerializeField]
        [Title("Заметки:")]
        [HideLabel]
        [TextArea]
        private string _notes;

#if UNITY_EDITOR
        private bool ValidateCollectablesNull(Dictionary<ECollectableType, List<Vector3>> _)
        {
            if (_collectablesSpawnInfo == null)
                _collectablesSpawnInfo = new();
            return true;
        }
#endif
    }
}