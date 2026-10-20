using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs
{
    [BurstCompile]
    public struct PlayerVsBoxesJob : IJob
    {
        // Player data
        public float3 PlayerPosition;
        public float3 PreviousPlayerPosition;
    
        // Box data arrays
        [ReadOnly] public NativeArray<float3> BoxCenters;
        [ReadOnly] public NativeArray<float3> BoxHalfExtents;
        [ReadOnly] public NativeArray<quaternion> BoxRotations;
    
        // Results
        public NativeArray<bool> IsPlayerInside;  // Current containment state per box
        public NativeArray<bool> PlayerEntered;   // Did player just enter each box
        public NativeArray<bool> PlayerExited;    // Did player just exit each box

        [BurstCompile]
        public void Execute()
        {
            // Более показать не могу
        }
    }
}