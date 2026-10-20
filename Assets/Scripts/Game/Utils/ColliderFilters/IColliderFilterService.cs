using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game.Utils.ColliderFilters
{
    public interface IColliderFilterService
    {
        public IDisposable Filter<T>(
            Vector3 position,
            Collider[] colliders,
            int obstacleLayerMask,
            int collidersCount,
            out List<T> components
        ) where T : Component;

        public IDisposable Filter<T>(
            Vector3 position,
            NativeArray<ColliderHit> colliders,
            int obstacleLayerMask,
            int collidersCount,
            out List<T> components
        ) where T : Component;
    }
}