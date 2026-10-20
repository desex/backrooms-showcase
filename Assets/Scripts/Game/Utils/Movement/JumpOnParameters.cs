using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct JumpOnParameters
    {
        public readonly Vector3 JumpEndPoint;
        public readonly Vector3 ObstacleForward;
        
        public JumpOnParameters(Vector3 jumpEndPoint, Vector3 obstacleForward)
        {
            JumpEndPoint = jumpEndPoint;
            ObstacleForward = obstacleForward;
        }
    }
}