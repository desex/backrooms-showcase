using Ecs.Managers;
using UnityEngine.Pool;

namespace Ecs.Scheduler.Extensions
{
	public static class SchedulerExtensions
	{
		public static SchedulerEntity CreateTimerAction(
			this SchedulerContext context,
			System.Action action,
			float timerSec,
			Uid? uid = null
		)
		{
			var entity = context.CreateEntity();
			entity.AddUid(uid ?? UidGenerator.Next());
			entity.AddAccumulator(0);
			entity.AddScheduledAction(action);
			entity.AddTimerSec(timerSec);
			entity.AddElapsedSec(timerSec);
			return entity;
		}

		public static SchedulerEntity CreateIntervalAction(
			this SchedulerContext context,
			System.Action action,
			float intervalSec,
			Uid? uid = null
		)
		{
			var entity = context.CreateEntity();
			entity.AddUid(uid ?? UidGenerator.Next());
			entity.AddIntervalAccumulator(0);
			entity.AddScheduledAction(action);
			entity.AddIntervalSec(intervalSec);
			return entity;
		}

		public static SchedulerEntity CreateIntervalActionWithTimer(
			this SchedulerContext context,
			System.Action action,
			int intervalSec,
			int timerSec,
			Uid? uid = null
		)
		{
			var entity = CreateIntervalAction(context, action, intervalSec, uid);
			entity.AddAccumulator(0);
			entity.AddTimerSec(timerSec);
			return entity;
		}

		public static SchedulerEntity CreateIntervalActionWithElapsed(
			this SchedulerContext context,
			System.Action action,
			float intervalSec,
			float elapsedSec,
			Uid? uid = null
		)
		{
			var entity = CreateIntervalAction(context, action, intervalSec, uid);
			entity.AddElapsedSec(elapsedSec);
			return entity;
		}

		public static SchedulerEntity GetEntityByName(this SchedulerContext context, string name)
		{
			using var _ = ListPool<SchedulerEntity>.Get(out var buffer);

			var group = context.GetGroup(SchedulerMatcher.AllOf(SchedulerMatcher.Name)
				.NoneOf(SchedulerMatcher.Destroyed));
			group.GetEntities(buffer);
			SchedulerEntity res = null;
			foreach (var schedulerEntity in buffer)
				if (schedulerEntity.Name.Value.Equals(name))
				{
					res = schedulerEntity;
					break;
				}

			return res;
		}
	}
}