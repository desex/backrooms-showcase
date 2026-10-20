using Core.Utils.TriggerCollision;
using Db.SoundFxBase;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Services.Obstacles;
using Game.Services.Player;
using Game.Services.Sound;
using Game.Utils;
using Game.Utils.Movement;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles.DynamicObstacles
{
    public partial class TravelatorModule : AObjectViewModule
    {
        [InfoBox("Optional trigger")]
        [SerializeField]
        private Box _travelatorTrigger;
        
        [SerializeField] private ETravelatorDirection _direction;
        [SerializeField] private ETravelatorSize _size;
        
        private GameContext _game;
        private System.Action _onTravelatorExit;
        private ITravelatorService _travelatorService;
        private IPlayerBoxTracker _playerBoxTracker;
        private IPlayerSpeedService _playerSpeedService;
        private ISoundService _soundService;
        private int _triggerUid;
        private bool _isTravelatorActive;
        
        [Inject]
        private void Construct(
            ITravelatorService travelatorService,
            IPlayerBoxTracker playerBoxTracker,
            IPlayerSpeedService playerSpeedService,
            ISoundService soundService,
            GameContext gameContext
        )
        {
            // Более показать не могу
        }
        
        public override void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }
        
        public override void Activate()
        {
            // Более показать не могу
        }
        
        public override void Deactivate()
        {
            // Более показать не могу
        }
        
        private void OnTravelatorEntered()
        {
            // Более показать не могу
        }
        
        private void OnTravelatorExited()
        {
            // Более показать не могу
        }
        
        private void DeactivateTravelator()
        {
            // Более показать не могу
        }
        
        private void ActivateTravelatorOnActionEnded(GameEntity entity)
        {
            // Более показать не могу
        }
        
        private void ActivateTravelator()
        {
            // Более показать не могу
        }
        
        private void HandlePlayerExitTravelator()
        {
            // Более показать не могу
        }
        
        private void HandlePlayerDashStartOnTravelator(GameEntity entity)
        {
            // Более показать не могу
        }
        
        private void HandlePlayerJumpingStartOnTravelator(GameEntity entity, EJumpType value)
        {
            // Более показать не могу
        }
    }
}