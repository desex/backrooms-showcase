using System;

namespace Game.Utils.Rebound
{
    [Flags]
    public enum EReboundDirection
    {
        None = 0,
        Back = 1 << 0,  
        Left = 1 << 1,
        Right = 1 << 2,
    }
}