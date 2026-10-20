using Game.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class ClimbHoleObstacleView : ObstacleGroupView
    {
        private GameContext _game;
        
        [Space]
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField] 
        private BoxCollider _dashTrigger;
        
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private Transform _climbEndPoint;
        
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private Transform _dashEndPoint;

        [Inject]
        private void Construct(GameContext game)
        {
            // Более показать не могу
        }
        
        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnDashTriggerEntered(Collider trigger)
        {
            // Более показать не могу
        }
        
        private void OnDashTriggerExit(Collider trigger)
        {
            // Более показать не могу
        }
    }
}