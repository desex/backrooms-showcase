using System;

namespace Ecs.Utils.ItemCollectables
{
    [Flags]
    public enum ECollectableType
    {
        None = 0,
        
        Cassette = 1 << 0,
        AlmondWater = 1 << 1,
        Bank = 1 << 2,
        
        Everything = int.MaxValue,
    }
}