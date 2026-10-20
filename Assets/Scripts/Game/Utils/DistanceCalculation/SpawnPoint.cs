using Ecs.Managers;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Utils.DistanceCalculation
{
    public struct SpawnPoint
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Uid ChunkUid;
        public readonly EChunkType ChunkType;

        public SpawnPoint(
            Vector3 position,
            Uid chunkUid,
            EChunkType chunkType,
            Quaternion rotation
        )
        {
            Position = position;
            ChunkUid = chunkUid;
            ChunkType = chunkType;
            Rotation = rotation;
        }
    }
}