#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Obstacles;
using Game.Utils.PrefabMatrix;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public sealed class SerializedChunkTypeInfo : IPrefabMatrix<ChunkView>
    {
        [ValidateInput("ValidateChunkSequenceCount")]
        public int ChunkSequenceMaxCount;
        
        [Header("Dictionary subscribes: Chunk type - Randomness weight")]
        [OdinSerialize]
        public Dictionary<EChunkType, int> NextChunkTypes = new();
        
        [Header("Dictionary subscribes: Obstacle type - Randomness weight")]
        [OdinSerialize]
        public Dictionary<EObstacleType, int> AvailableObstacles = new();

        [MinMaxSlider(0, 10, true)]
        public Vector2Int ObstaclesCount;

        [LabelText("Chunks")]
        [ValidateInput("@PrefabMatrixUtils.ValidateMatrix<ChunkView>(this, ChunksMatrix)")]
        [PrefabTableMatrix] 
        [OdinSerialize]
        public GameObject[,] ChunksMatrix = new GameObject[PrefabMatrixUtils.ROW_AMOUNT, 1];

        public GameObject[,] PrefabMatrix 
        {
            get => ChunksMatrix;
            set => ChunksMatrix = value;

        }
        public ChunkView[] ToArray()
        { 
            return ChunksMatrix.Cast<GameObject>()
                               .Where(x => x != null)
                               .Where(x => x.TryGetComponent<ChunkView>(out _))
                               .Select(x => x.GetComponent<ChunkView>())
                               .ToArray();
        }

        public static implicit operator ChunkTypeInfo(SerializedChunkTypeInfo serializedChunkTypeInfo)
        {
            return new ChunkTypeInfo
            {
                IsGenerative = false,
                ChunksGenerationInfo = new ChunkGenerationInfo(serializedChunkTypeInfo.ChunkSequenceMaxCount, 
                                                               serializedChunkTypeInfo.NextChunkTypes),
                AvailableObstacles = serializedChunkTypeInfo.AvailableObstacles,
                ObstaclesCount = serializedChunkTypeInfo.ObstaclesCount,
                Chunks = serializedChunkTypeInfo.ToArray(),
            };
        }
        
        [ShowIf("@ChunksMatrix.GetLength(1) > 1")]
        [Button("Delete last row")]
        private void DeleteLastRow()
        {
            PrefabMatrixUtils.DeleteLastRow(this);
        }
        
        private bool ValidateChunkSequenceCount()
        {
            if (ChunkSequenceMaxCount < 1)
                ChunkSequenceMaxCount = 1;

            return true;
        }
    }
}
#endif