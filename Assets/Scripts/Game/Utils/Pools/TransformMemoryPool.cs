using UnityEngine;
using Zenject;

namespace Game.Utils.Pools
{
	public class TransformMemoryPool<TValue> : MemoryPool<TValue>
		where TValue : Component
	{
		private static readonly Vector3 DefaultPosition = new Vector3(0, -5000, 0);

		protected Transform OriginalParent;

		protected override void OnCreated(TValue item)
		{
			if (item is IDespawnable<TValue> despawnable)
				despawnable.SetDespawn(Despawn);

			var tr = item.transform;
			tr.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
			// Record the original parent which will be set to whatever is used in the UnderTransform method
			if (OriginalParent == null)
				OriginalParent = tr.parent;
#endif
		}

		protected override void OnDestroyed(TValue item)
		{
			Object.Destroy(item.gameObject);
		}

		protected override void OnSpawned(TValue item)
		{
//            item.gameObject.SetActive(true);
		}

		protected override void OnDespawned(TValue item)
		{
			var tr = item.transform;
			tr.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
			var parent = tr.transform.parent;
			if (OriginalParent == null && parent == null)
				return;
			if (OriginalParent == null && parent != null
			    || parent.GetInstanceID() != OriginalParent.GetInstanceID())
			{
				tr.transform.SetParent(OriginalParent, false);
			}
#endif
		}
	}
}