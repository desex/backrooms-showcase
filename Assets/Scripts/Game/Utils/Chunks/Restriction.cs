using System;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct Restriction
    {
        public EWallRestrictionType RestrictionType;
        public EChunkWallType RestrictionArgument;
    }
}