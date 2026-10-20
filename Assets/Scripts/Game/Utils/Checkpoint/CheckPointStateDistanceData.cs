using System;
using Sirenix.OdinInspector;

namespace Game.Utils.Checkpoint
{
    [Serializable]
    public struct CheckPointStateDistanceData
    {
        public ECheckpointTvState State;
        [field: SuffixLabel("meters", Overlay = true)] public float Distance;
    }
}