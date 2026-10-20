using UnityEngine;

namespace Game.Utils.HoleGenerator
{
    [System.Serializable]
    public struct ObstaclePassSettingsParameters
    {
        public GameObject Prefab;
        public float Depth;
        public float OffsetWidth;
        public float OffsetHeight;
        public float DistanceBetweenObjects;
    }
}