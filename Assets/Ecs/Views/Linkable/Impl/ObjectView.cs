using System.Collections.Generic;
using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using JCMG.EntitasRedux.Core.View.Impls;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl
{
    public class ObjectView : LinkableView, IObjectLinkable
    {
        [SerializeField] private List<AObjectViewModule> modules;

        public List<AObjectViewModule> Modules => modules;
        public Transform Transform => transform;
        protected virtual bool IsDestroyOnUnlink => true;

        [Button]
        public void FillModules()
        {
            // Более показать не могу
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        public virtual void Activate()
        {
            // Более показать не могу
        }

        public virtual void Deactivate()
        {
            // Более показать не могу
        }

        protected virtual void OnPosition(GameEntity entity, Vector3 value)
        {
            // Более показать не могу
        }

        protected void OnLocalPosition(GameEntity entity, Vector3 value)
        {
            // Более показать не могу
        }

        protected virtual void OnRotation(GameEntity entity, Quaternion value)
        {
            // Более показать не могу
        }

        protected virtual void OnVelocity(GameEntity entity, Vector3 value)
        {
            // Более показать не могу
        }

        protected void SetEntityPosition(GameEntity entity)
        {
            // Более показать не могу
        }

        protected void SetEntityLocalPosition(GameEntity entity)
        {
            // Более показать не могу
        }

        protected override void OnClear()
        {
            // Более показать не могу
        }
    }
}