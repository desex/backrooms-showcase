using System;
using UnityEngine;

namespace Core.Utils.TriggerCollision
{
    [Serializable]
    public struct Box
    {
        public Vector3 Center;
        public Vector3 Size;
        public Vector3 Rotation;
    }
}