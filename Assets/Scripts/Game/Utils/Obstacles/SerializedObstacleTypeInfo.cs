#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;

namespace Game.Utils.Obstacles
{
    [Serializable]
    [HideReferenceObjectPicker]
    public sealed class SerializedObstacleTypeInfo
    {
        public bool IsObstacleWithWalls;
        [HideLabel] public ObstaclesMatrix Obstacles = new ();

        public static implicit operator ObstacleTypeInfo(SerializedObstacleTypeInfo serializedObstacleTypeInfo)
        {
            return new ObstacleTypeInfo()
            {
                Obstacles = serializedObstacleTypeInfo.Obstacles.ToArray(),
                IsObstacleWithWalls = serializedObstacleTypeInfo.IsObstacleWithWalls,
            };
        }
    }
}
#endif