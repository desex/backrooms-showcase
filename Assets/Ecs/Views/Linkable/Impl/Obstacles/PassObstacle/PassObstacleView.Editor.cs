#if UNITY_EDITOR
using Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Obstacles.PassObstacle
{
    public partial class PassObstacleView
    {
        [Space]
        [SerializeField]
        [TabGroup(GENERAL_GROUP)]
        private float _obstacleLength;

        [Space]
        [TabGroup(GIZMOS_GROUP)]
        [SerializeField] 
        private Transform _startMovePoint;
        
        [TabGroup(GIZMOS_GROUP)]
        [SerializeField] 
        private Color _movePathColor = Color.red;

        [TabGroup(GIZMOS_GROUP)] 
        [SerializeField]
        private Transform _obstacleModelRoot;
        
        [TabGroup(GIZMOS_GROUP)] 
        [SerializeField]
        private Transform _obstacleRoot;
        
        [TabGroup(GIZMOS_GROUP)] 
        [SerializeField]
        private bool _isReverseDashRotation;
        
        [TabGroup(GIZMOS_GROUP)]
        [SerializeField] 
        private Color _dashColor = Color.yellow;

        protected override float PivotToBottomLength => _obstacleLength / 2;
        
        protected override void AdditionalDrawGizmos()
        {
            // Более показать не могу
        }

        private void DrawDash()
        {
            // Более показать не могу
        }

        private void DrawMove()
        {
            // Более показать не могу
        }
        
        private void DrawInactivityTrigger()
        {
            // Более показать не могу
        }
    }
}
#endif