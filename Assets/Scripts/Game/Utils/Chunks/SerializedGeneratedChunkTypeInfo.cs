#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public sealed class SerializedGeneratedChunkTypeInfo
    {
        [ValidateInput("ValidateChunkSequenceCount")]
        public int ChunkSequenceMaxCount;
        
        public ChunkView EmptyChunk;
        
        [Header("Dictionary subscribes: Chunk type - Randomness weight")]
        [OdinSerialize]
        public Dictionary<EChunkType, int> NextChunkTypes = new();

        [Header("Dictionary subscribes: Obstacle type - Randomness weight")]
        [OdinSerialize]
        public Dictionary<EObstacleType, int> AvailableObstacles = new();

        [MinMaxSlider(0, 10, true)]
        public Vector2Int ObstaclesCount;
        
        public string[] WallZonePresetGroupsNames;

        public static implicit operator ChunkTypeInfo(SerializedGeneratedChunkTypeInfo serializedGeneratedChunkTypeInfo)
        {
            return new ChunkTypeInfo()
            {
                IsGenerative = true,
                ChunksGenerationInfo = new ChunkGenerationInfo(serializedGeneratedChunkTypeInfo.ChunkSequenceMaxCount, 
                                                               serializedGeneratedChunkTypeInfo.NextChunkTypes),
                Chunks = new [] { serializedGeneratedChunkTypeInfo.EmptyChunk },
                AvailableObstacles = serializedGeneratedChunkTypeInfo.AvailableObstacles,
                ObstaclesCount = serializedGeneratedChunkTypeInfo.ObstaclesCount,
                WallZonePresetGroupsNames = serializedGeneratedChunkTypeInfo.WallZonePresetGroupsNames,
            };
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