using UnityEngine;

namespace Game.Utils.Chunks
{
    public readonly struct ChunkAnchors
    {
        public readonly Transform StartAnchor;
        public readonly Transform EndOnlyAnchor;
        public readonly Transform EndLeftAnchor;
        public readonly Transform EndRightAnchor;
        public readonly Transform[] MiddleAnchors;
        
        public ChunkAnchors(Transform startAnchor, Transform endOnlyAnchor, Transform endLeftAnchor, Transform endRightAnchor, Transform[] middleAnchors)
        {
            StartAnchor = startAnchor;
            EndOnlyAnchor = endOnlyAnchor;
            EndLeftAnchor = endLeftAnchor;
            EndRightAnchor = endRightAnchor;
            MiddleAnchors = middleAnchors;
        }
    }
}