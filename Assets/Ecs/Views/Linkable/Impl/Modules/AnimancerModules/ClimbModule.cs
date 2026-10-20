using Ecs.Utils;
using Game.Utils;
using Game.Utils.Animancer;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class ClimbModule : AObjectViewModule
    {
        [SerializeField] private MotionClipTransition _climbAnimation;

        private GameEntity _self;

        private float _climbAnimationOriginSpeed;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }
    }
}