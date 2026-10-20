using System;
using System.Collections.Generic;
using System.Linq;
using JCMG.EntitasRedux;
using Tests.Playable.UITest.Conditions;

namespace Tests.Playable.UITest.Custom.Conditions.Entity
{
	public class EntityMatch<TEntity> : ICondition where TEntity : class, IEntity
	{
		private readonly IEntityMatchCase<TEntity> _matchCase;
		private readonly List<TEntity> _entities = new List<TEntity>();

		public EntityMatch(IEntityMatchCase<TEntity> matchCase)
		{
			_matchCase = matchCase;
		}

		public bool Satisfied()
		{
			var contexts = Contexts.SharedInstance.AllContexts;
			var context = contexts.FirstOrDefault(f => f is IContext<TEntity>) as IContext<TEntity>;
			if (context == null)
				throw new Exception($"[EntityMatch] Cannot find context for entity {typeof(TEntity).Name}");

			var entities = context.GetEntities();
			_entities.Clear();
			foreach (var entity in entities)
			{
				if (entity == null || !_matchCase.Filter(entity))
					continue;
				_entities.Add(entity);
			}

			return _matchCase.Match(_entities);
		}

		public override string ToString() => $"EntityMatch({_matchCase})";
	}
}