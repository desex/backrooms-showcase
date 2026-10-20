using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Overlaps
{
    public interface IOverlapService
    {
        IDisposable OverlapBox<T>(
            Vector3 position,
            Vector3 square,
            int searchLayerMask,
            out List<T> overlapBox,
            int obstacleLayerMask = 0
        ) where T : Component;

        IDisposable OverlapSphere<T>(
            Vector3 position,
            float radius,
            int searchLayerMask,
            out List<T> overlapSphere
        ) where T : Component;

        bool IsOverlapBoxColliding(
            Vector3 position,
            Vector3 size,
            Quaternion quaternion,
            int searchLayerMask
        );
        
        bool CheckOverlapSphere(
            Vector3 position, 
            float radius, 
            int searchLayerMask
        );
    }
}