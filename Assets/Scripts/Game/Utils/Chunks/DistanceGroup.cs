using System;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct DistanceGroup
    {
        public int GroupLength;
        public EChunkType GroupFilter;
    }
}