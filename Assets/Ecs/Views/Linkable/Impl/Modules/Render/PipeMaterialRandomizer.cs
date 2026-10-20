using Ecs.Utils;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.Render
{
    [RequireComponent(typeof(MeshRandomColorSetter))]
    public sealed class PipeRandomColorModule : AObjectViewModule
    {
        [SerializeField] private MeshRandomColorSetter meshRandomColorSetter;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
    }
}