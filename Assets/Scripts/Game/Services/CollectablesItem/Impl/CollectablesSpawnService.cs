using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.CollectablesItem.Data;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.ItemCollectables;
using Ecs.Views.Linkable.Impl.Collectables;
using Game.Providers;
using UnityEngine;
using Zenject;

namespace Game.Services.CollectablesItem.Impl
{
    public partial class CollectablesSpawnService : ICollectableSpawnService
    {
        private const float CLOSE_ENOUGH_THRESHOLD = 0.1f;

        private readonly ICollectablesPool _collectablesItemPool;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly ILinkedEntityRepository _linkedEntityRepository;

        private readonly List<CollectableSpawnPointData> _points = new List<CollectableSpawnPointData>();
        private readonly List<CollectableSpawner> _spawners = new List<CollectableSpawner>();

        private float _distanceFromStartUnits;

        public CollectablesSpawnService(
            IInstantiator instantiator,
            ICollectablesPool collectablesItemPool,
            IGameFieldProvider gameFieldProvider,
            ILinkedEntityRepository linkedEntityRepository,
            GameContext game
        )
        {
            InitSpawners(instantiator);
            _collectablesItemPool = collectablesItemPool;
            _gameFieldProvider = gameFieldProvider;
            _linkedEntityRepository = linkedEntityRepository;
            _game = game;
        }

        public void Add(Vector3 position)
        {
            // Более показать не могу
        }

        public async UniTask SpawnCollectable(
            ECollectableType collectableType,
            Vector3 position,
            CancellationToken token
        )
        {
            // Более показать не могу
        }

        public async UniTask DespawnCollectable(Uid collectableUid, CancellationToken token)
        {
            // Более показать не могу
        }

        private void InitSpawners(IInstantiator instantiator)
        {
            // Более показать не могу
        }
    }
}