using System.Collections.Generic;
using Ecs.Views.Linkable;
using UnityEngine;

namespace Game.Services.Pool.Impls
{
    public class PrefabPoolService : IPrefabPoolService
    {
        private readonly Dictionary<string, IPrefabMemoryPool> _prefabPools =
            new Dictionary<string, IPrefabMemoryPool>();

        public PrefabPoolService(List<IPrefabMemoryPool> prefabPools)
        {
            foreach (var pool in prefabPools)
                _prefabPools.Add(pool.Name, pool);
        }

        public bool Spawn(string prefab, Vector3 position, Quaternion rotation, out IObjectLinkable objectLinkable)
        {
            objectLinkable = null;

            if (!_prefabPools.TryGetValue(prefab, out var pool))
                return false;

            objectLinkable = pool.Spawn();
            objectLinkable.Transform.SetPositionAndRotation(position, rotation);
            return true;
        }
    }
}