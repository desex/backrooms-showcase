using Ecs.Utils;
using Game.Services.Start;
using Game.Utils.Animancer;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.AnimancerModules
{
    public class TransitionModule : AObjectViewModule
    {
        [Inject] private readonly IGameStartService _gameStartService;
        
        [SerializeField] private MotionClipTransition _enterTransitionAnimation;
        [SerializeField] private MotionClipTransition _exitTransitionAnimation;

        private GameEntity _self;

        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnLevelExit(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }

        private void OnLevelEnter(GameEntity entity, EBodyMoveType value)
        {
            // Более показать не могу
        }
    }
}