using System;
using System.Collections.Generic;
using Game.Utils.Linecasts;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Utils.ColliderFilters.Impl
{
    public class ColliderFilterService : IColliderFilterService
    {
        private readonly ILinecastProvider _linecastProvider;

        public ColliderFilterService(ILinecastProvider linecastProvider)
        {
            _linecastProvider = linecastProvider;
        }

        public IDisposable Filter<T>(
            Vector3 position,
            Collider[] colliders,
            int obstacleLayerMask,
            int collidersCount,
            out List<T> components
        ) where T : Component
        {
            var compsDisposable = ListPool<T>.Get(out components);
            components.Clear();

            for (var i = 0; i < collidersCount; i++)
            {
                var collider = colliders[i];
                
                if (!collider.TryGetComponent(out T comp))
                    continue;

                var hitPos = collider.transform.position;
                
                if (_linecastProvider.Linecast(position, hitPos, obstacleLayerMask))
                    continue;

                components.Add(comp);
            }

            return compsDisposable;
        }

        public IDisposable Filter<T>(
            Vector3 position,
            NativeArray<ColliderHit> colliders,
            int obstacleLayerMask,
            int collidersCount,
            out List<T> components
        ) where T : Component
        {
            var compsDisposable = ListPool<T>.Get(out components);
            components.Clear();

            for (var i = 0; i < collidersCount; i++)
            {
                var hit = colliders[i];
                
                if (!hit.collider)
                    continue;
                
                if (!hit.collider.TryGetComponent(out T comp))
                    continue;

                components.Add(comp);
            }

            return compsDisposable;
        }
    }
}
