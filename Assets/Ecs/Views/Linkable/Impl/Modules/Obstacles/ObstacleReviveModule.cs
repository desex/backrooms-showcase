using Core.Utils.TriggerCollision;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Services.Revive;
using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Modules.Obstacles
{
    public partial class ObstacleReviveModule : AObjectViewModule
    {
        [SerializeField]
        private Box _trigger;

        private GameContext _game;
        private IPlayerBoxTracker _playerBoxTracker;

        private IReviveObstacleService _reviveObstacleService;
        private GameEntity _self;
        private int _triggerUid;

        [Inject]
        public void Construct(
            IReviveObstacleService reviveObstacleService,
            IPlayerBoxTracker playerBoxTracker,
            GameContext game
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
    }
}