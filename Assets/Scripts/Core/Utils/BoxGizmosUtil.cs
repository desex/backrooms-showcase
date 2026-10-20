using Core.Utils.TriggerCollision;
using UnityEngine;

namespace Core.Utils
{
    public static class BoxGizmosUtil
    {
        public static void Draw(Color color, Transform transform, Box box)
        {
            Gizmos.color = color;
            var originalMatrix = Gizmos.matrix;

            var worldPosition = transform.TransformPoint(box.Center);
            var worldRotation = transform.rotation * Quaternion.Euler(box.Rotation);
            var worldScale = Vector3.Scale(transform.lossyScale, box.Size);
            Gizmos.matrix = Matrix4x4.TRS(worldPosition, worldRotation, worldScale);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = originalMatrix;
        }
    }
}