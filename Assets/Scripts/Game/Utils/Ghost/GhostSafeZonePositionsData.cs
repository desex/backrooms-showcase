using UnityEngine;

namespace Game.Utils.Ghost
{
    public readonly struct GhostSafeZonePositionsData
    {
        public readonly Vector3 LeftPoint;
        public readonly Vector3 RightPoint;

        public GhostSafeZonePositionsData(
            Vector3 leftPoint, 
            Vector3 rightPoint
        )
        {
            LeftPoint = leftPoint;
            RightPoint = rightPoint;
        }
    }
}