using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public abstract class ObjectLinkableUiView<TEntity> : UiView, IObjectLinkableUi<TEntity>
        where TEntity : class, IEntity
    {
        private TEntity _entity;
        private bool _destroyed;
        
        private readonly UnsubscribeEvent _unsubscribeEvent = new();

        public int Hash => transform.GetHashCode();
        public Transform Transform => transform;

        public void Link(IEntity entity)
        {
            // Более показать не могу
        }

        public void Unlink()
        {
            // Более показать не могу
        }

        void IObjectLinkableUi<TEntity>.Reset()
        {
            // Более показать не могу
        }

        protected virtual void OnReset()
        {
            
        }
        
        private void OnDestroyEntity(IEntity entity)
        {
            // Более показать не могу
        }

        protected virtual void OnClear()
        {
        }
        
        public virtual void Subscribe(TEntity entity, IUnsubscribeEvent unsubscribe){}

        public void Unsubscribe()
        {
            // Более показать не могу
        }

        protected virtual void OnUnsubscribe()
        {
        }

        protected override void OnDestroy()
        {
            // Более показать не могу
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}