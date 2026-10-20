using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using UnityEngine.Animations;

namespace Ecs.Views.Linkable.Impl.Modules.Player
{
    public class PlayerCameraOffsetModule : AObjectViewModule
    {
        [SerializeField] private ParentConstraint _cameraParentConstraint;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void UpdateCameraOffset(GameEntity player, float targetOffset)
        {
            // Более показать не могу
        }
    }
}