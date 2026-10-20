
using UnityEngine;

namespace Game.Utils.Linecasts
{
    public interface ILinecastProvider
    {
        bool Linecast(Vector3 position, Vector3 colliderPosition, int obstacleLayerMask);
    }
}