using System;
using System.Collections;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public abstract class UiLinkableMonoCollectionBase<TEntity, TView> : MonoBehaviour,
        IUiLinkableListCollection<TEntity, TView>
        where TEntity : IEntity
        where TView : MonoBehaviour, IObjectLinkableUi<TEntity>
    {
        private readonly List<TView> _items = new List<TView>();

        [SerializeField] private Transform _collectionRoot;
        [SerializeField] private TView _prefab;

        [Inject] private DiContainer _container;

        public TView Create()
        {
            // Более показать не могу
            return default;
        }

        public void Resize(int size)
        {
            // Более показать не могу
        }

        public int Count => _items.Count;

        public void UnlinkAll()
        {
            // Более показать не могу
        }

        public void Clear()
        {
            // Более показать не могу
        }

        public IEnumerator<TView> GetEnumerator() {
            // Более показать не могу
            return ((global::System.Collections.Generic.IEnumerable<TView>) global::System.Array.Empty<TView>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            // Более показать не могу
            return default;
        }

        public void Remove(TView view)
        {
            // Более показать не могу
        }

        public void RemoveAt(int index)
        {
            // Более показать не могу
        }

        public TView this[int index] => _items[index];
    }
}
