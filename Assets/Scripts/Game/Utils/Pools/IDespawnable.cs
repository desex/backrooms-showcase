using System;

namespace Game.Utils.Pools
{
	public interface IDespawnable<out TItem>
	{
		void SetDespawn(Action<TItem> despawn);
	}
}