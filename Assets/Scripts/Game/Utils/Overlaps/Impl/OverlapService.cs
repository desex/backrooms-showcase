using System;
using System.Collections.Generic;
using Game.Utils.ColliderFilters;
using Unity.Collections;
using UnityEngine;

namespace Game.Utils.Overlaps.Impl
{
    public class OverlapService : IOverlapService, IDisposable
    {
        private const int MAX_COLLIDERS_COUNT = 255;

        private readonly Collider[] _boxColliders = new Collider[MAX_COLLIDERS_COUNT];
        private readonly Collider[] _singleCollider = new Collider[1];
        private readonly IColliderFilterService _colliderFilterService;

        private NativeArray<ColliderHit> _colliderHits = new(MAX_COLLIDERS_COUNT, Allocator.Persistent);

        public OverlapService(IColliderFilterService colliderFilterService)
        {
            _colliderFilterService = colliderFilterService;
        }

        public bool CheckOverlapSphere(Vector3 position, float radius, int searchLayerMask)
        {
            var commands = new NativeArray<OverlapSphereCommand>(1, Allocator.TempJob);
            var colliderHitsForGrounded = new NativeArray<ColliderHit>(1, Allocator.TempJob);
            
            var parameters = new QueryParameters(searchLayerMask, hitTriggers: QueryTriggerInteraction.UseGlobal);
            commands[0] = new OverlapSphereCommand(position, radius, parameters);
            
            OverlapSphereCommand
               .ScheduleBatch(
                    commands, 
                    colliderHitsForGrounded, 
                    1, 
                    1
               )
               .Complete();
            
            if (colliderHitsForGrounded[0].collider != null)
            {
                colliderHitsForGrounded.Dispose();
                commands.Dispose();
                return true;
            }
            
            colliderHitsForGrounded.Dispose();
            commands.Dispose();
            return false;
        }
        
        public IDisposable OverlapSphere<T>(
            Vector3 position,
            float radius,
            int searchLayerMask,
            out List<T> overlap
        )
            where T : Component
        {
            var colliderHits = OverlapSphere(position, radius, searchLayerMask);
            
            if (colliderHits.Length == 0)
                return UnityEngine.Pool.ListPool<T>.Get(out overlap);
            
            var disposable = _colliderFilterService.Filter(
                position,
                colliderHits,
                searchLayerMask,
                colliderHits.Length,
                out overlap
            );
            
            return disposable;
        }

        public IDisposable OverlapBox<T>(
            Vector3 position,
            Vector3 square,
            int searchLayerMask,
            out List<T> overlapBox,
            int obstacleLayerMask = 0
        ) where T : Component
        {
            var collidersCount = OverlapBox(position, square, searchLayerMask);

            if (collidersCount == 0)
                return UnityEngine.Pool.ListPool<T>.Get(out overlapBox);

            var disposable = _colliderFilterService.Filter(
                position,
                _boxColliders,
                obstacleLayerMask,
                collidersCount,
                out overlapBox
            );

            return disposable;
        }
        
        public bool IsOverlapBoxColliding(Vector3 position, Vector3 size, Quaternion quaternion, int searchLayerMask)
        {
            var halfExtents = size / 2f;

            var countOfCollisions = Physics.OverlapBoxNonAlloc(
                position,
                halfExtents,
                _singleCollider,
                quaternion,
                searchLayerMask,
                QueryTriggerInteraction.Ignore
            );

            return countOfCollisions > 0;
        }

        private int OverlapBox(Vector3 position, Vector3 square, int searchLayerMask)
        {
            return Physics.OverlapBoxNonAlloc(
                position,
                square,
                _boxColliders,
                Quaternion.identity,
                searchLayerMask
            );
        }

        private NativeArray<ColliderHit> OverlapSphere(Vector3 position, float radius, int searchLayerMask)
        {
            var commands = new NativeArray<OverlapSphereCommand>(1, Allocator.TempJob);

            var parameters = new QueryParameters(searchLayerMask, hitTriggers: QueryTriggerInteraction.UseGlobal);
            commands[0] = new OverlapSphereCommand(position, radius, parameters);

            OverlapSphereCommand
                .ScheduleBatch(commands, _colliderHits, 1, MAX_COLLIDERS_COUNT)
                .Complete();

            commands.Dispose();

            return _colliderHits;
        }

        public void Dispose()
        {
            _colliderHits.Dispose();
        }
    }
}