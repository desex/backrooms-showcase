using System;
using UnityEngine;

namespace Game.Utils.Tiling
{
    [Serializable]
    public struct TilingData
    {
        public Vector2 Tiling;
        public Vector2 Offset;
        public int Index;
    }
}