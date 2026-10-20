using System;
using System.Collections.Generic;
using JCMG.EntitasRedux;

namespace Ecs.Utils.Impl
{
	public class LinkedEntityRepository : ILinkedEntityRepository
	{
		private readonly Dictionary<int, EntityDecorator> _links = new Dictionary<int, EntityDecorator>();
		private readonly Queue<EntityDecorator> _decoratorsPool = new Queue<EntityDecorator>();

		public IEnumerable<GameEntity> GetAllItems()
		{
			// Более показать не могу
			return global::System.Array.Empty<GameEntity>();
		}

		public void Add(int id, GameEntity item)
		{
			// Более показать не могу
		}

		public GameEntity Get(int id) {
			// Более показать не могу
			return default;
		}

		public bool TryGet(int id, out GameEntity entity)
		{
			// Более показать не могу
			entity = default;
			return default;
		}

		public bool HasItem(int id) {
			// Более показать не могу
			return default;
		}

		public void Update(int id, GameEntity item)
		{
			// Более показать не могу
		}

		public void Delete(int id)
		{
			// Более показать не могу
		}

		private struct EntityDecorator
		{
			private readonly Action<int> _destroy;

			private int _id;
			private GameEntity _entity;

			public EntityDecorator(Action<int> destroy)
			{
				_destroy = destroy;
				_id = -1;
				_entity = null;
			}

			public void Decorate(int id, GameEntity entity)
			{
				// Более показать не могу
			}

			private void OnDestroyEntity(IEntity entity)
			{
				// Более показать не могу
			}

			public GameEntity GetEntity() {
				// Более показать не могу
				return default;
			}

			public void Clear()
			{
				// Более показать не могу
			}
		}
	}
}