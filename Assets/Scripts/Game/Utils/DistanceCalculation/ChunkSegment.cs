using System.Collections.Generic;
using Ecs.Managers;
using Game.Utils.Chunks;
using UnityEngine;

namespace Game.Utils.DistanceCalculation
{
    public class ChunkSegment
    {
        private readonly List<Vector3> _middlePoints = new();

        public EChunkType ChunkType { get; private set; }

        public Uid ThisUId { get; private set; }
        public ESegment Segment { get; private set; }

        public Vector3 Start { get; set; }
        public Vector3 Next1 { get; set; }
        public Vector3 Next2 { get; private set; }

        public float DistanceToNext1 { get; set; }
        public float DistanceToNext2 { get; set; }

        public float StartDistance { get; set; }
        public float EndDistance { get; set; }

        public IReadOnlyList<Vector3> MiddlePoints => _middlePoints;

        public ChunkSegment(in ChunkSegmentInitializeData initializeData)
        {
            Initialize(initializeData);
        }

        public void Initialize(in ChunkSegmentInitializeData initializeData)
        {
            // Более показать не могу
        }

        public void AddMiddlePoint(Vector3 middlePoint) {
            // Более показать не могу
        }

        public void Reset()
        {
            // Более показать не могу
        }
    }
}