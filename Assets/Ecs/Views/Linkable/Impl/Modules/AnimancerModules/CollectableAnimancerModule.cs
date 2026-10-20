using Animancer;
using Ecs.Utils;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CollectableAnimancerModule : AObjectViewModule
    {
        [Required]
        [SerializeField]
        private AnimancerComponent _animancer;
        
        [Header("Animation Clips")]
        [SerializeField]
        private MotionClipTransition _clipTransition;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnActive(GameEntity entity)
        {
            // Более показать не могу
        }
    }
}