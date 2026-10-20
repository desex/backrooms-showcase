using System;
using System.Collections;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using Tests.Playable.UITest.Conditions;

namespace Tests.Playable.UITest.Custom.Conditions.Entity
{
	public interface IEntityMatchCase<TEntity>
		where TEntity : class, IEntity
	{
		bool Filter(TEntity entity);
		bool Match(List<TEntity> entities);
	}

	public class EntityMatchCase<TEntity> : IEntityMatchCase<TEntity>
		where TEntity : class, IEntity
	{
		private readonly List<TEntity> _entitiesBuffer = new List<TEntity>();

		private string _conditionName;
		private string _notMatchCase;
		private Func<TEntity, bool> _filter;
		private Func<int, bool> _countCondition;
		private Func<TEntity, bool> _entityMatchCondition;
		private Func<List<TEntity>, bool> _customMatchCondition;

		public bool Filter(TEntity entity) => _filter?.Invoke(entity) ?? true;

		public bool Match(List<TEntity> entities)
		{
			_entitiesBuffer.Clear();
			foreach (var entity in entities)
			{
				if (!(entity is TEntity castedEntity))
					continue;
				_entitiesBuffer.Add(castedEntity);
			}

			if (!_customMatchCondition?.Invoke(_entitiesBuffer) ?? false)
			{
				_notMatchCase = "Unexpected custom match case";
				return false;
			}

			if (!_countCondition?.Invoke(_entitiesBuffer.Count) ?? false)
			{
				_notMatchCase = "Unexpected count case, actual count: " + _entitiesBuffer.Count;
				return false;
			}

			foreach (var entity in _entitiesBuffer)
			{
				if (_entityMatchCondition?.Invoke(entity) ?? true)
					continue;
				_notMatchCase = "Unexpected entity match case";
				return false;
			}

			_notMatchCase = "Success";
			return true;
		}

		public override string ToString() => $"{_conditionName} : {_notMatchCase}";

		public class Builder
		{
			private readonly EntityMatchCase<TEntity> _entityMatchCase;
			private float _timeoutS = 2;
			private bool _checkContinuously;

			public Builder()
			{
				_entityMatchCase = new EntityMatchCase<TEntity>();
			}

			public Builder SetConditionName(string name)
			{
				_entityMatchCase._conditionName = name;
				return this;
			}

			public Builder SetEntityFilter(Func<TEntity, bool> filter)
			{
				_entityMatchCase._filter = filter;
				return this;
			}

			public Builder SetCount(int count)
			{
				_entityMatchCase._countCondition = entitiesCount => entitiesCount == count;
				return this;
			}
			
			public Builder SetCountCondition(Func<int, bool> countCondition)
			{
				_entityMatchCase._countCondition = countCondition;
				return this;
			}

			public Builder SetEntityMatch(Func<TEntity, bool> match)
			{
				_entityMatchCase._entityMatchCondition = match;
				return this;
			}

			public Builder SetCustomMatch(Func<List<TEntity>, bool> customMatch)
			{
				_entityMatchCase._customMatchCondition = customMatch;
				return this;
			}

			public Builder SetTimeout(float timeoutS)
			{
				_timeoutS = timeoutS;
				return this;
			}
			
			public Builder SetContinuousChecking(bool checkContinuously)
			{
				_checkContinuously = checkContinuously;
				return this;
			}

			public IEnumerator Build()
			{
				if (_checkContinuously)
					return new ContinuousConditionChecking(new EntityMatch<TEntity>(_entityMatchCase), _timeoutS)
					   .GetEnumerator();
				
				return new WaitCondition(new EntityMatch<TEntity>(_entityMatchCase), _timeoutS)
					.GetEnumerator();
			}
		}
	}
}