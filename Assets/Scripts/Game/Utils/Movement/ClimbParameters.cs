using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct ClimbParameters
    {
        public readonly Vector3 StartPoint;
        public readonly Vector3 EndPoint;
        public readonly float High;

        public ClimbParameters(
            Vector3 startPoint, 
            Vector3 endPoint, 
            float high
        )
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            High = high;
        }
    }
}