using Ecs.Utils;
using Game.Utils;
using Game.Utils.Characters;
using Generated.Commands;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using R3;
using R3.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public class HoleWithPlankObstacleView : ObstacleGroupView
    {
        private const string TRIGGER_HEADER = "Triggers";

        [Space]
        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private Transform _startPoint;

        [Required]
        [TabGroup(GENERAL_GROUP)]
        [Header(TRIGGER_HEADER)]
        [SerializeField]
        private BoxCollider _tighteningTrigger;

        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private BoxCollider _beamExitTrigger;

        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private BoxCollider _decelerationTrigger;

        [Required]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private BoxCollider _speedReturnTrigger;

        private ICommandBuffer _commandBuffer;
        private ILinkedEntityRepository _linkedEntityRepository;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, ILinkedEntityRepository linkedEntityRepository)
        {
            // Более показать не могу
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            // Более показать не могу
        }

        private void OnTighteningTriggerEntered(Collider trigger)
        {
            // Более показать не могу
        }

        private void OnBeamExitTriggerExited(Collider trigger)
        {
            // Более показать не могу
        }

        private void OnDecelerationTriggerEntered(Collider trigger)
        {
            // Более показать не могу
        }

        private void OnSpeedReturnTriggerEntered(Collider trigger)
        {
            // Более показать не могу
        }
    }
}