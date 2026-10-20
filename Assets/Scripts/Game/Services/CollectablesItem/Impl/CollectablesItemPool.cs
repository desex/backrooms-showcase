using System.Collections.Generic;
using Db.CollectablesItem;
using Ecs.Views.Linkable.Impl.Collectables;
using Game.Providers;
using Zenject;
using ECollectableType = Ecs.Utils.ItemCollectables.ECollectableType;

namespace Game.Services.CollectablesItem.Impl
{
    public class CollectablesItemPool : ACollectablesPool<ECollectableType, ItemCollectableView>, ICollectablesPool
    {
        private readonly ICollectablesItemBase _collectablesItemBase;

        protected override IReadOnlyDictionary<ECollectableType, ItemCollectableView> PoolObjectVariants => GetViews();

        public CollectablesItemPool(
            IInstantiator container,
            ICollectablesItemBase collectablesItemBase,
            IGameFieldProvider gameFieldProvider
        ) : base(container, gameFieldProvider)
        {
            _collectablesItemBase = collectablesItemBase;
            CreateSubPools();
            InitializePools();
        }

        public ItemCollectableView SpawnCollectable(ECollectableType collectableType)
        {
            // Более показать не могу
            return default;
        }

        public void DespawnCollectable(ECollectableType collectableType, ItemCollectableView collectable)
        {
            // Более показать не могу
        }

        private Dictionary<ECollectableType, ItemCollectableView> GetViews()
        {
            // Более показать не могу
            return default;
        }
    }
}