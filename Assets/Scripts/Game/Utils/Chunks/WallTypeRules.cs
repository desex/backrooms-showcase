using System;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct WallTypeRules
    {
        public EChunkWallType WallType;
        public Restriction[] Restrictions;
    }
}