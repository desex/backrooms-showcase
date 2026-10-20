using System;
using System.Collections.Generic;
using Game.Utils.Checkpoint;
using Game.Utils.Chunks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Checkpoints.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CheckpointsParameters), fileName = nameof(CheckpointsParameters))]
    public class CheckpointsParameters : SerializedScriptableObject, ICheckpointsParameters
    {
        private const string SPAWN_PARAMETERS_GROUP = "Spawn parameters";
        private const string ANIMATION_BLACK_SCREEN_FOLDOUT_GROUP = "Animations/Black screen";
        private const string ANIMATION_NOISE_FOLDOUT_GROUP = "Animations/Noise";
        private const string ANIMATION_NEW_RECORD_FOLDOUT_GROUP = "Animations/New Record";
        private const string ANIMATION_DEFAULT_FOLDOUT_GROUP = "Animations/Default";
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SerializeField] 
        public int RadiusObstacleSpawnCheckInDefault{ get; private set; }
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)] 
        [field: SerializeField] 
        public int RadiusObstacleSpawnCheckInSequential { get; private set; }
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SerializeField] 
        public int FrequencyCheckpointsInMeters { get; private set; }
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SerializeField] 
        public int MeterToKilometerThreshold { get; private set; }
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SerializeField] 
        public float ForwardAngleThreshold { get; private set; } = 90f;
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SuffixLabel("frames", Overlay = true)]
        [field: SerializeField] 
        public int ParentDelayAfterCreation { get; private set; } = 2;
        
        [field: BoxGroup(SPAWN_PARAMETERS_GROUP)]
        [field: SerializeField] 
        public List<EChunkType> SpawnChunks { get; private set; }
        
        [field: BoxGroup("States")]
        [field: SerializeField] 
        public CheckPointStateDistanceData[] StateDistances { get; private set; }
        
        [field: PropertySpace(4)]
        [field: BoxGroup("Animations")]
        [field: FoldoutGroup(ANIMATION_BLACK_SCREEN_FOLDOUT_GROUP)]
        [field: PreviewField]
        [field: SerializeField] 
        public Texture2D BlackScreenTexture { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_BLACK_SCREEN_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int BlackScreenTextureSize { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_BLACK_SCREEN_FOLDOUT_GROUP)]
        [field: SerializeField] 
        public CheckpointTexturesData NoiseTexturesData { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_NOISE_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int AppearNoiseTextureSize { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_NOISE_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField]
        public Vector2Int LoopNoiseTextureSize { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_NEW_RECORD_FOLDOUT_GROUP)]
        [field: PropertySpace(4)]
        [field: PreviewField]
        [field: SerializeField] 
        public Texture2D NewRecordTexture { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_NEW_RECORD_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int AppearNewRecordTextureSize { get; private set; } 
        
        [field: FoldoutGroup(ANIMATION_NEW_RECORD_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int LoopNewRecordTextureSize { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_DEFAULT_FOLDOUT_GROUP)]
        [OdinSerialize] 
        private Dictionary<int, CheckpointTexturesData> _texturesData = new();
        
        [field: FoldoutGroup(ANIMATION_DEFAULT_FOLDOUT_GROUP)]
        [field: PropertySpace(4)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int AppearDefaultTextureSize { get; private set; }
        
        [field: FoldoutGroup(ANIMATION_DEFAULT_FOLDOUT_GROUP)]
        [field: MinMaxSlider(1, 10, true)]
        [field: SerializeField] 
        public Vector2Int LoopDefaultTextureSize { get; private set; }
        
        public IReadOnlyDictionary<int, CheckpointTexturesData> TexturesData => _texturesData;

#if UNITY_EDITOR
        [FoldoutGroup("Animations/Editor")]
        [Button]
        private void FillTextures(int maxDistance, Texture2D[] textures)
        {
            // Более показать не могу
        }

        private bool ContainsTexture(Texture2D texture, out int key)
        {
            // Более показать не могу
            key = default;
            return default;
        }
#endif
    }
}