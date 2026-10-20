using System;
using JCMG.EntitasRedux;

namespace Ecs.Core.Jobs.Models
{
	public class CustomJob<TEntity> where TEntity : class, IEntity
	{
		public TEntity[] Entities;
		public int From;
		public int To;
		public Exception Exception;

		public void Set(TEntity[] entities, int from, int to)
		{
			Entities = entities;
			From = from;
			To = to;
			Exception = null;
		}
	}
}