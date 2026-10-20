using Ecs.Views.Linkable;
using UnityEngine;
using Zenject;

namespace Game.Services.Pool.Impls
{
    public class PrefabMemoryPool : MemoryPool<IObjectLinkable>, IPrefabMemoryPool
    {
        private static readonly Vector3 DefaultPosition = new Vector3(0, -5000, 0);

        protected Transform OriginalParent;

        public PrefabMemoryPool(string name)
        {
            Name = name;
        }

        public string Name { get; }

        protected override void OnCreated(IObjectLinkable item)
        {
            var tr = item.Transform;
            tr.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR

            // Record the original parent which will be set to whatever is used in the UnderTransform method
            if (OriginalParent == null)
                OriginalParent = tr.parent;
#endif
        }

        protected override void OnDestroyed(IObjectLinkable item)
        {
            Object.Destroy(item.Transform.gameObject);
        }

        protected override void OnSpawned(IObjectLinkable item)
        {
            //            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(IObjectLinkable item)
        {
            var tr = item.Transform;
            tr.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
            var parent = tr.transform.parent;

            if (OriginalParent == null && parent == null)
                return;

            if (OriginalParent == null && parent != null || parent.GetInstanceID() != OriginalParent.GetInstanceID())
                tr.transform.SetParent(OriginalParent, false);
#endif
        }
    }
}