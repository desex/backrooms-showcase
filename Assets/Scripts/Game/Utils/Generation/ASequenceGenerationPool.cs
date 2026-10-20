using System.Collections.Generic;
using Ecs.Managers;
using Ecs.Views.Linkable.Impl;
using Game.Services.PoolCleaner;
using R3;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Utils.Generation
{
    public abstract class ASequenceGenerationPool<TType, TObject> : ISequenceGenerationPool<TObject>, IClearablePool
        where TType : struct
        where TObject : PoolObjectView
    {
        private const string SEARCH_FOLDER = "Assets/";
        
        private readonly IInstantiator _container;
        
        private Dictionary<int, ObjectPool<TObject>> _pools = new();

        private readonly ReactiveCommand<Uid> _viewOfEntityDestroyedCommand = new();
        public Observable<Uid> OnViewOfEntityDestroyed => _viewOfEntityDestroyedCommand;
        
        protected abstract IReadOnlyDictionary<TType, IReadOnlyList<TObject>> PoolObjectVariants { get; }
        protected virtual int MaxPoolSize => 10;

        protected ASequenceGenerationPool(IInstantiator container)
        {
            _container = container;
        }

        public void CreateSubPools()
        {
            _pools = new();

            foreach (var objectVariantPair in PoolObjectVariants)
            {
                foreach (var prefab in objectVariantPair.Value)
                {
                    var pool = new ObjectPool<TObject>(
                        () => _container.InstantiatePrefab(prefab).GetComponent<TObject>(),
                        view =>
                        {
                            view.gameObject.SetActive(true);
                            view.transform.SetFarAwayPosition();
                        },
                        view =>
                        {
                            view.gameObject.SetActive(false);
                        },
                        view =>
                        {
                            if (view.EntityId != null)
                                _viewOfEntityDestroyedCommand.Execute(view.EntityId.Value);
                            Object.Destroy(view.gameObject);
                        }, false, 3, MaxPoolSize);

                    if (_pools.ContainsKey(prefab.gameObject.GetInstanceID()))
                    {
                        Debug.LogError($"Pool with prefab {prefab.gameObject.name} already exist! Check double links in configs");
                        continue;
                    }

                    _pools.Add(prefab.gameObject.GetInstanceID(), pool);
                }
            }
        }

        public TObject SpawnObject(int instanceID)
        {
            return instanceID == 0
                ? null
                : Get(instanceID);
        }
        
        public void DespawnObject(int instanceID, TObject objectView)
        {
            _pools[instanceID].Release(objectView);
        }
        
        public void Clear()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }

            _pools.Clear();
        }
        
        private TObject Get(int instanceID)
        {
            var objectPool = _pools.TryGetValue(instanceID, out var pool)
                ? pool
                : LogMissedPrefab(instanceID);

            return objectPool.Get();
        }
        
        private static ObjectPool<TObject> LogMissedPrefab(int instanceID)
        {
#if UNITY_EDITOR
            Debug.LogError($"Pool with prefab {FindGameObjectNameByInstanceId(instanceID)} not found! Add it to configs");
#endif
            return null;
        }
        
#if UNITY_EDITOR
        private static string FindGameObjectNameByInstanceId(int targetInstanceID)
        {
            var assets = AssetDatabase.FindAssets("t:prefab", new[] { SEARCH_FOLDER });

            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (targetInstanceID == prefab.GetInstanceID())
                    return path;
            }

            return "Not found";
        }
#endif
    }
}