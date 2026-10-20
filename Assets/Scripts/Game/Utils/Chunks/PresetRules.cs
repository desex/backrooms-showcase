using System;

namespace Game.Utils.Chunks
{
    [Serializable]
    public struct PresetRules
    {
        public bool CanSpawnObstacles;
        public WallTypeRules[] WallTypeRules;
    }
}