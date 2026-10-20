using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct ChunkGenerationInfo
    {
        [SerializeField] private Dictionary<EChunkType, int> _nextChunkTypes;

        [field: SerializeField] public int ChunkSequenceCount { get; private set; }
        public IReadOnlyDictionary<EChunkType, int> NextChunkTypes => _nextChunkTypes;

        public ChunkGenerationInfo(int chunkSequenceCount, Dictionary<EChunkType, int> nextChunkTypes)
        {
            ChunkSequenceCount = chunkSequenceCount;
            _nextChunkTypes = nextChunkTypes;
        }
    }
}