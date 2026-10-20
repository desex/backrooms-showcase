using System;
using Ecs.Utils;
using Game.Services.Player;
using Game.Utils.Animancer;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using PdUtils.RandomProvider;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class CharacterReceiveDamageAnimancerModule : AObjectViewModule
    {
        [Header("Damage")]
        [SerializeField] private MotionClipTransition _hitObstacleRightSide;
        [SerializeField] private MotionClipTransition _hitObstacleLeftSide;
        
        [Header("Dead")]
        [SerializeField] private MotionClipTransition _dead;
        
        private GameEntity _self;
        
        private IRandomProvider _randomProvider;
        private IPlayerMovementTypeService _playerMovementTypeService;

        [Inject]
        private void Construct(
            IRandomProvider randomProvider,
            IPlayerMovementTypeService playerMovementTypeService
        )
        {
            // Более показать не могу
        }

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnAfterHit()
        {
            // Более показать не могу
        }

        private void OnHitObstacle(GameEntity entity, EHitDirection hitDirection)
        {
            // Более показать не могу
        }
        
        private void OnDead(GameEntity player)
        {
            // Более показать не могу
        }
    }
}