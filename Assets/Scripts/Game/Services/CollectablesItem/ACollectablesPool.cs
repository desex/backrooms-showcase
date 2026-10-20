using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Collectables;
using Game.Providers;
using Game.Utils;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Services.CollectablesItem
{
    public abstract class ACollectablesPool<TType, TObject> : ICollectablesItemPool<TType, TObject>
        where TType : unmanaged where TObject : ItemCollectableView
    {
        private readonly IInstantiator _container;
        private readonly IGameFieldProvider _gameFieldProvider;

        private Dictionary<TType, ObjectPool<TObject>> _pools;

        protected abstract IReadOnlyDictionary<TType, TObject> PoolObjectVariants { get; }
        protected virtual int MaxPoolSize => 10;
        protected virtual int ObjectsPreCreateCount => 50;

        protected ACollectablesPool(IInstantiator container, IGameFieldProvider gameFieldProvider)
        {
            _container = container;
            _gameFieldProvider = gameFieldProvider;
        }

        public TObject Spawn(TType type)
        {
            // Более показать не могу
            return default;
        }

        public void Despawn(TType type, TObject objectView)
        {
            // Более показать не могу
        }

        public void CreateSubPools()
        {
            // Более показать не могу
        }

        public void InitializePools()
        {
            // Более показать не могу
        }

        public void Clear()
        {
            // Более показать не могу
        }
    }
}