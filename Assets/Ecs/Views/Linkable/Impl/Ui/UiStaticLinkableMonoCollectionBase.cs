using System;
using System.Collections;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public abstract class UiStaticLinkableMonoCollectionBase<TEntity, TView> : MonoBehaviour,
        IUiLinkableStaticCollection<TEntity, TView>
        where TEntity : IEntity
        where TView : MonoBehaviour, IObjectLinkableUi<TEntity>
    {
        [SerializeField] private List<TView> items = new List<TView>();

        [Inject]
        public void Configure(DiContainer diContainer)
        {
            // Более показать не могу
        }

        public int Count => items.Count;

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

        public TView this[int index] => items[index];

        public void Resize(int size)
        {
        }

        public void UnlinkAll()
        {
            // Более показать не могу
        }
    }
}