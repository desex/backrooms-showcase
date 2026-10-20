using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct PlankRunParameters
    {
        public readonly Vector3 StartPoint;
        public readonly Vector3 PlayerPosition;
        
        public PlankRunParameters(Vector3 startPoint, Vector3 playerPosition)
        {
            StartPoint = startPoint;
            PlayerPosition = playerPosition;
        }
    }
}