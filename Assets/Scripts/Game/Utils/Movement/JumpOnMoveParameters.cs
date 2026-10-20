using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct JumpOnMoveParameters
    {
        public readonly Vector3 JumpEndPoint;
        public readonly Vector3 PlayerStartPosition;
        
        public JumpOnMoveParameters(Vector3 jumpEndPoint, Vector3 playerStartPosition)
        {
            JumpEndPoint = jumpEndPoint;
            PlayerStartPosition = playerStartPosition;
        }
    }
}