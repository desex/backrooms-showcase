using System.Collections.Generic;
using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Core.Systems
{
	public abstract class AChangeParameterSystem<TValue, EObservedEntity, UTargetEntity>
		: ReactiveSystem<EObservedEntity>
		where EObservedEntity : class, IEntity, IDestroyedEntity
		where UTargetEntity : class, IEntity, IUidEntity
	{
		private readonly IContext<UTargetEntity> _target;
		private readonly Dictionary<Uid, TValue> _targetToTotal = new();

		protected AChangeParameterSystem(
			IContext<EObservedEntity> observed,
			IContext<UTargetEntity> target
		) : base(observed)
		{
			_target = target;
		}

		protected override void Execute(IEnumerable<EObservedEntity> entities)
		{
			_targetToTotal.Clear();
			foreach (var action in entities)
			{
				action.IsDestroyed = true;

				var targetUid = GetTarget(action);
				var delta = GetDelta(action);
				if (_targetToTotal.ContainsKey(targetUid))
				{
					var val = _targetToTotal[targetUid];
					var sum = Add(val, delta);
					_targetToTotal[targetUid] = sum;
				}
				else
				{
					_targetToTotal.Add(targetUid, delta);
				}
			}

			foreach (var entry in _targetToTotal)
			{
				var target = GetEntityWithUid(_target, entry.Key);
				UpdateParameter(target, entry.Value);
			}
		}

		protected abstract UTargetEntity GetEntityWithUid(IContext<UTargetEntity> context, Uid uid);

		protected abstract Uid GetTarget(EObservedEntity action);

		protected abstract TValue GetDelta(EObservedEntity action);

		protected abstract TValue Add(TValue @base, TValue added);

		protected abstract void UpdateParameter(UTargetEntity entity, TValue delta);
	}
}