using Core.Utils.TriggerCollision;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs
{
    [BurstCompile]
    public struct CapsuleBoxTransitionJob : IJobParallelFor
    {
        private const float THRESHOLD = 1e-6f;

        [ReadOnly]
        public CapsuleData CapsuleData;

        [ReadOnly]
        public CapsuleData PrevCapsuleData;

        [ReadOnly]
        public NativeArray<BoxData>.ReadOnly BoxesData;

        public NativeArray<bool> IsPlayerInside;
        public NativeArray<bool> PlayerEntered;
        public NativeArray<bool> PlayerExited;

        [ReadOnly]
        public int InterpolationSteps;

        [BurstCompile]
        public void Execute(int i)
        {
            // Более показать не могу
        }

        private void CheckFrame(in CapsuleData capsuleData, bool isInitialFrame, int i)
        {
            // Более показать не могу
        }

        private static bool CapsuleBoxIntersection(in float3 p1, in float3 p2, float radius, in float3 boxHalfExtents)
        {
            // Более показать не могу
            return default;
        }

        private static float3 ClosestPointSegmentToAABB(in float3 p1, in float3 p2, in float3 boxHalfExtents)
        {
            // Более показать не могу
            return default;
        }
    }
}