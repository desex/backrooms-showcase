using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct MonkeyJumpParameters
    {
        public readonly Vector3 StartPoint;
        public readonly Vector3 EndPoint;

        public MonkeyJumpParameters(Vector3 startPoint, Vector3 endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}