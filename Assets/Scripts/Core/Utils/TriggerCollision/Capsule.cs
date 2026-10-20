using Unity.Mathematics;
using UnityEngine;

namespace Core.Utils.TriggerCollision
{
    public struct Capsule
    {
        public float Radius;
        public float Height;
        public float3 Center;
        public Quaternion Rotation;
    }
}