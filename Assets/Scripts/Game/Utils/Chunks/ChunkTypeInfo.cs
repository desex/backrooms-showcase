using System;
using System.Collections;
using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Obstacles;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public sealed class ChunkTypeInfo : IReadOnlyList<ChunkView>
    {
        public bool IsGenerative;
        public ChunkGenerationInfo ChunksGenerationInfo;
        public ChunkView[] Chunks;
        [OdinSerialize] public Dictionary<EObstacleType, int> AvailableObstacles;
        public Vector2Int ObstaclesCount;
        public string[] WallZonePresetGroupsNames;

        public int Count => Chunks.Length;
        
        public ChunkView this[int index] => Chunks[index];
        
        public IEnumerator<ChunkView> GetEnumerator()
        {
            return (Chunks as IReadOnlyList<ChunkView>).GetEnumerator(); //TODO: rider annotate that must be disposed, if will be allocation - check
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Chunks.GetEnumerator();
        }
    }
}