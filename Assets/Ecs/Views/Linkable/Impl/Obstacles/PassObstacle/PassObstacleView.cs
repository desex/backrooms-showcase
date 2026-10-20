using Core.Utils.TriggerCollision;
using DG.Tweening;
using Ecs.Utils.ObstaclePass;
using Ecs.Views.Linkable.Impl.Modules.Obstacles.Jobs;
using Game.Utils;
using Game.Utils.Movement;
using Game.Utils.Obstacles;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Obstacles.PassObstacle
{
    public partial class PassObstacleView : AObstacleGroupWithMoveZonesView
    {
        [Space]
        [Header("Pass obstacle parameters:")]
        [SerializeField]
        private EObstaclePassType _obstaclePassType;

        [Space]
        [Header("Obstacle type for acceleration")]
        [SerializeField]
        private EObstacleTypeForAcceleration _obstacleTypeForAcceleration;

        [SerializeField]
        private PassObstacleMovePointsData[] _obstaclePassPoints;

        [SerializeField]
        private Transform _centerStartReadyTriggerPosition;

        [SerializeField]
        [Range(0, 1f)]
        private float _passObstacleFastMoveMultiplier;

        [InfoBox("Optional trigger")]
        [SerializeField]
        private Box _passObstacleInactivityTrigger;

        [Space]
        [Header("Dash parameters:")]
        [SerializeField]
        private bool _isDashEnabled = true;

        [ShowIf(nameof(_isDashEnabled))]
        [SerializeField]
        private float _dashDuration;

        [ShowIf(nameof(_isDashEnabled))]
        [SerializeField]
        private Transform _dashEndPosition;

        [ShowIf(nameof(_isDashEnabled))]
        [SerializeField]
        private Ease _dashEase;

        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private bool _disableInput;

        [ShowIf(nameof(_isDashEnabled))]
        [SerializeField]
        [Range(0, 1f)]
        private float _dashFastMoveMultiplier;

        private ICommandBuffer _commandBuffer;
        private IPlayerBoxTracker _playerBoxTracker;
        private int _triggerUid;

        public PassObstacleMovePointsData[] ObstaclePassPoints => _obstaclePassPoints;

        [Inject]
        private void Construct(ICommandBuffer commandBuffer, IPlayerBoxTracker playerBoxTracker)
        {
            // Более показать не могу
        }

        protected override void LinkBoxActions()
        {
            // Более показать не могу
        }

        protected override void UnlinkBoxActions()
        {
            // Более показать не могу
        }

        protected override void StretchBoxesSize(float stretchSize)
        {
            // Более показать не могу
        }

        private void OnInactivityTriggerEnter()
        {
            // Более показать не могу
        }

        protected override void OnReadyTriggerEntered()
        {
            // Более показать не могу
        }

        protected override void OnReadyTriggerExited()
        {
            // Более показать не могу
        }

        protected override void OnFastActionMoveTriggerEntered()
        {
            // Более показать не могу
        }

        protected override void OnFastActionMoveTriggerExited()
        {
            // Более показать не могу
        }
    }
}