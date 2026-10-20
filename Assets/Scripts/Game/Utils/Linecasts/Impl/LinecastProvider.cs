using UnityEngine;

namespace Game.Utils.Linecasts.Impl
{
    public class LinecastProvider : ILinecastProvider
    {
        public bool Linecast(Vector3 position, Vector3 colliderPosition, int obstacleLayerMask) =>
            Physics.Linecast(position, colliderPosition, obstacleLayerMask);
    }
}