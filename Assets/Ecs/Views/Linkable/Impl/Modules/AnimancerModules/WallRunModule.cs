using Ecs.Utils;
using Game.Utils.Animancer;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class WallRunModule : AObjectViewModule
    {
        [SerializeField] private MotionLinearMixerTransition _wallRunAnimation;
        
        private GameEntity _self;
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            var self = (GameEntity)entity;
            
            self.SubscribeBodyMoveType(OnBodyMoveType).AddTo(unsubscribe);
        }
        
        private void OnBodyMoveType(GameEntity entity, EBodyMoveType value)
        {
            if (value != EBodyMoveType.WallRun)
                return;

            var animancer = entity.Animancer.Value;
            animancer.Play(_wallRunAnimation);
            
            var slide = entity.CharacterTouchWall.WallType == ETiltType.Left ? -1f : 1f;

            _wallRunAnimation.State.Parameter = slide;
        }
        
    }
}