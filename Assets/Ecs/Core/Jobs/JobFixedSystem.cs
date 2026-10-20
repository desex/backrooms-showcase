using System;
using System.Threading;
using Ecs.Core.Interfaces;
using Ecs.Core.Jobs.Models;
using JCMG.EntitasRedux;

namespace Ecs.Core.Jobs
{
	public abstract class JobFixedSystem<TEntity> : IFixedSystem where TEntity : class, IEntity
	{
		private readonly IGroup<TEntity> _group;
		private readonly int _threads;
		private readonly CustomJob<TEntity>[] _jobs;
		private int _threadsRunning;

		protected JobFixedSystem(IGroup<TEntity> group, int threads)
		{
			_group = group;
			_threads = threads;
			_jobs = new CustomJob<TEntity>[threads];
			for (var index = 0; index < _jobs.Length; ++index)
				_jobs[index] = new CustomJob<TEntity>();
		}

		public void Fixed()
		{
			_threadsRunning = _threads;
			var entities = _group.GetEntities();
			var num1 = entities.Length % _threads;
			var num2 = entities.Length / _threads + (num1 == 0 ? 0 : 1);
			for (var index = 0; index < _threads; ++index)
			{
				var from = index * num2;
				var to = from + num2;
				if (to > entities.Length)
					to = entities.Length;
				_jobs[index].Set(entities, from, to);
				if (from != to)
					ThreadPool.QueueUserWorkItem(queueOnThread, _jobs[index]);
				else
					Interlocked.Decrement(ref _threadsRunning);
			}

			do
			{
				;
			} while (_threadsRunning != 0);

			foreach (var job in _jobs)
				if (job.Exception != null)
					throw job.Exception;
		}

		private void queueOnThread(object state)
		{
			var job = (CustomJob<TEntity>)state;
			try
			{
				for (var from = job.From; from < job.To; ++from)
					Execute(job.Entities[from]);
			}
			catch (Exception ex)
			{
				job.Exception = ex;
			}
			finally
			{
				Interlocked.Decrement(ref _threadsRunning);
			}
		}

		protected abstract void Execute(TEntity entity);
	}
}