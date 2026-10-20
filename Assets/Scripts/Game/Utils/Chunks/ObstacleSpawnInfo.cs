using UnityEngine;

namespace Game.Utils.Chunks
{
    public readonly struct ObstacleSpawnInfo
    {
        public readonly Vector3 Position;
        public readonly Vector3 Right;
        public readonly Quaternion Rotation;
        public readonly float LeftOffset;
        public readonly float RightOffset;
        public readonly int? WallIndex;

        public ObstacleSpawnInfo(
            Vector3 position, 
            Vector3 right, 
            Quaternion rotation, 
            float leftOffset, 
            float rightOffset,
            int? wallIndex
        )
        {
            Position = position;
            Right = right;
            Rotation = rotation;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
            WallIndex = wallIndex;
        }
        
        public static implicit operator ObstacleSpawnInfo(ObstacleSpawnPoint spawnPoint)
        {
            return new ObstacleSpawnInfo(
                spawnPoint.Point.position, 
                spawnPoint.Point.right, 
                spawnPoint.Point.rotation, 
                spawnPoint.LeftOffset, 
                spawnPoint.RightOffset,
                null
            );
        }
    }
}