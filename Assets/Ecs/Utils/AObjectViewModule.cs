using Ecs.Views.Linkable.Impl;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Utils
{
    public abstract class AObjectViewModule : MonoBehaviour
    {
        public virtual void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            
        }

        public virtual void Activate()
        {
            
        }

        public virtual void Deactivate()
        {
            
        }
        
        public virtual void DrawGizmos()
        {
            
        }
    }
}