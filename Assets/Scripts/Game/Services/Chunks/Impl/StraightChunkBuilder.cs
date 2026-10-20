using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public sealed class StraightChunkBuilder : AChunkBuilder
    {
        public StraightChunkBuilder(ChunkView chunk) : base(chunk)
        {
        }

        public override void AddWalls(ChunkWallView leftWall, ChunkWallView rightWall)
        {
            Chunk.ChunkWalls ??= new List<ChunkWallView>();

            AddWallToRoot(leftWall);
            AddWallToRoot(rightWall, true);

            RootPosition = Chunk.ChunkWalls[^2].EndAnchor.position;
        }

        public override void BuildChunkAnchors()
        {
            Chunk.EndOnlyAnchor.position = RootPosition;
        }

        private void AddWallToRoot(ChunkWallView wall, bool isMirrored = false)
        {
            wall.SetMirroredState(isMirrored);

            var wallTransform = wall.transform;

            wallTransform.position = RootPosition;
            wallTransform.rotation = RootRotation;
            wallTransform.localScale = new Vector3(isMirrored ? -1 : 1, 1, 1);

            wallTransform.parent = ChunkTransform;

            Chunk.ChunkWalls.Add(wall);
        }
    }
}