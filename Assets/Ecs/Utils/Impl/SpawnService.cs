using System;
using Db.Prefabs;
using Ecs.Views.Linkable;
using Game.Services.Pool;
using UnityEngine;
using Zenject;

namespace Ecs.Utils.Impl
{
	public class SpawnService : ISpawnService<GameEntity, IObjectLinkable>
	{
		private readonly DiContainer _container;
		private readonly IPrefabsBase _prefabsBase;
		private readonly IPrefabPoolService _prefabPoolService;

		public SpawnService(
			DiContainer container,
			IPrefabsBase prefabsBase,
			IPrefabPoolService prefabPoolService
		)
		{
			_container = container;
			_prefabsBase = prefabsBase;
			_prefabPoolService = prefabPoolService;
		}

		public IObjectLinkable Spawn(GameEntity entity)
		{
			// Более показать не могу
			return default;
		}

		private IObjectLinkable InstantiateLinkable(Vector3 position, GameObject prefab)
		{
			// Более показать не могу
			return default;
		}
	}
}