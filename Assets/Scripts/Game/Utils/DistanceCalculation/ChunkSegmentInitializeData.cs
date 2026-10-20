using UnityEngine;

namespace Game.Utils.DistanceCalculation
{
    public ref struct ChunkSegmentInitializeData
    {
        public GameEntity ChunkEntity;
        public ESegment Segment;
        public Vector3 Start;
        public Vector3 Next1;
        public Vector3 Next2;
        public float DistanceToNext1;
        public float DistanceToNext2;
        public float StartDistance;
        public float EndDistance;
    }
}