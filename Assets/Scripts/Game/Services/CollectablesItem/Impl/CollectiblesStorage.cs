using System;
using System.Collections.Generic;
using Ecs.Utils.ItemCollectables;
using Game.DaoData.Collectibles;
using PdUtils.Dao;
using R3.Utils;
using Zenject;

namespace Game.Services.CollectablesItem.Impl
{
    public class CollectiblesStorage : ICollectiblesStorage, IInitializable
    {
        private readonly IDao<CollectiblesStorageData> _collectiblesStorageData;

        private readonly ReactiveDictionary<ECollectableType, int> _itemsCollection =
            new ReactiveDictionary<ECollectableType, int>();

        public CollectiblesStorage(IDao<CollectiblesStorageData> collectiblesStorageData)
        {
            _collectiblesStorageData = collectiblesStorageData;
        }

        public IReadOnlyReactiveDictionary<ECollectableType, int> ItemsCollection => _itemsCollection;

        public void AddSingleCollectibleItem(ECollectableType type)
        {
            // Более показать не могу
        }

        public void AddCollectibleItem(ECollectableType type, int value)
        {
            // Более показать не могу
        }

        public void RemoveCollectibleItem(ECollectableType type, int count)
        {
            // Более показать не могу
        }

        public bool HasCollectibleItem(ECollectableType type, int count)
        {
            // Более показать не могу
            return default;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        private void Save()
        {
            // Более показать не могу
        }

        private void Load()
        {
            // Более показать не могу
        }

        private CollectiblesStorageData LoadDefaultData()
        {
            // Более показать не могу
            return default;
        }
    }
}