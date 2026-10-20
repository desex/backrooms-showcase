using Animancer;
using Db.Monsters;
using Ecs.Utils;
using Game.Services.Chaser;
using Game.Utils.Animancer;
using Game.Utils.QTE;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Monsters
{
    public class MonsterAnimationModule : AObjectViewModule
    {
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private MotionClipTransition _run;
        [SerializeField] private GameObject _hideObject;
        
        private IChaserPresentationService _chaserPresentationService;
        private IChaserPresentationParameters _chaserPresentationParameters;
        private AnimancerState _state;
        private GameContext _game;

        [Inject]
        private void Construct(
            IChaserPresentationService chaserPresentationService,
            IChaserPresentationParameters chaserPresentationParameters,
            ICommandBuffer commandBuffer,
            GameContext game
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnQteTypeRemoved(GameEntity entity)
        {
            // Более показать не могу
        }

        private void OnQteType(GameEntity entity, EQteType value)
        {
            // Более показать не могу
        }

        private void OnPlayerRevived(GameEntity entity)
        {
            // Более показать не могу
        }

        private void OnHandShowed(GameEntity entity)
        {
            // Более показать не могу
        }

        private void Move()
        {
            // Более показать не могу
        }

        private void IncreaseAnimationSpeed(Unit unit)
        {
            // Более показать не могу
        }        
        
        private void DecreaseAnimationSpeed(Unit unit)
        {
            // Более показать не могу
        }

        private void Appear()
        {
            // Более показать не могу
        }

        private void Disappear()
        {
            // Более показать не могу
        }
    }
}