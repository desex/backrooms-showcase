using Ecs.Views.Linkable.Impl.Chunk;
using UnityEngine;

namespace Game.Services.Chunks
{
    public abstract class AChunkBuilder
    {
        protected readonly ChunkView Chunk;
        protected readonly Transform ChunkTransform;
        protected Vector3 RootPosition;
        protected Quaternion RootRotation;

        protected AChunkBuilder(ChunkView chunk)
        {
            Chunk = chunk;
            ChunkTransform = chunk.transform;
            RootPosition = ChunkTransform.position;
            RootRotation = ChunkTransform.rotation;
        }

        public abstract void AddWalls(ChunkWallView leftWall, ChunkWallView rightWall);
        public abstract void BuildChunkAnchors();
    }
}