using Ecs.Managers;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Utils.DistanceCalculation
{
    public struct ChunkEndAnchor
    {
        public readonly Vector3 Position;
        public readonly float Distance;
        public readonly Uid ChunkUid;
        public readonly EChunkType ChunkType;

        public ChunkEndAnchor(
            Vector3 position,
            float distance,
            Uid chunkUid,
            EChunkType chunkType
        )
        {
            Position = position;
            Distance = distance;
            ChunkUid = chunkUid;
            ChunkType = chunkType;
        }
    }
}