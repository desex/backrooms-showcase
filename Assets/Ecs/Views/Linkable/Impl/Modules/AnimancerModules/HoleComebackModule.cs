using Ecs.Utils;
using Game.Utils.Animancer;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class HoleComebackModule : AObjectViewModule
    {
        [Inject] private readonly ICommandBuffer _commandBuffer;
        
        [SerializeField] private MotionClipTransition _handsAnimation;

        [SerializeField] private MotionClipTransition _comebackAnimation;

        private GameEntity _self;

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