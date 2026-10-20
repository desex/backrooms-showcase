#if UNITY_EDITOR
using Core.Utils;
using Game.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Obstacles
{
    public partial class AObstacleGroupWithMoveZonesView : IFastActionTrigger
    {
        protected const string GIZMOS_GROUP = "Gizmos";
        
        [TabGroup(GIZMOS_GROUP)]
        [SerializeField] 
        private Color _readyTriggerColor = Color.green;
        
        [TabGroup(GIZMOS_GROUP)]
        [SerializeField]
        private Color32 _fastMoveTriggerColor = new Color32(59, 164, 73, 127);
        
        [Space]
        [OnValueChanged(nameof(ChangeReadyTriggerHeight))]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private float _readyTriggerHeight;
        
        [OnValueChanged(nameof(ChangeReadyTriggerWidth))]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private float _readyTriggerWidth;
        
        private float _readyTriggerOffset;

        [Space]
        [OnValueChanged(nameof(ChangeSlowMoveTriggerHeight))]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private float _fastActionMoveTriggerHeight;
        
        [OnValueChanged(nameof(ChangeSlowMoveTriggerWidth))]
        [TabGroup(GENERAL_GROUP)]
        [SerializeField]
        private float _fastActionMoveTriggerWidth;
        
        private float _fastActionMoveTriggerOffset;
        
        protected abstract float PivotToBottomLength { get; }
        
        public float ReadyTriggerOffset 
        {
            set
            {
                _readyTriggerOffset = value;
                ObstacleBoxTriggerRecalculator.ChangeTriggerOffset(_readyBoxTrigger, _readyTriggerOffset, PivotToBottomLength);
            }
        }

        private float _readyTriggerLength;
        public float ReadyTriggerLength {
            set
            {
                _readyTriggerLength = value;
                ObstacleBoxTriggerRecalculator.ChangeTriggerLength(_readyBoxTrigger, _readyTriggerLength, _readyTriggerOffset, PivotToBottomLength);
            }
        }
        
        public float FastMoveTriggerOffset 
        {
            set
            {
                _fastActionMoveTriggerOffset = value;
                ObstacleBoxTriggerRecalculator.ChangeTriggerOffset(_fastActionMoveBoxTrigger, _fastActionMoveTriggerOffset, PivotToBottomLength);
            }
        }
        
        private float _fastActionMoveTriggerLength;
        public float FastMoveTriggerLength 
        {
            set
            {
                _fastActionMoveTriggerLength = value;
                ObstacleBoxTriggerRecalculator.ChangeTriggerLength(_fastActionMoveBoxTrigger, _fastActionMoveTriggerLength, _fastActionMoveTriggerOffset, PivotToBottomLength);
            }
        }
        
        protected override void OnDrawGizmosSelected()
        {
            // Более показать не могу
        }
        
        private void OnDrawGizmos()
        {
            // Более показать не могу
        }

        protected virtual void AdditionalDrawGizmos()
        {
        }

        private void ChangeReadyTriggerHeight()
        {
            // Более показать не могу
        }
        
        private void ChangeReadyTriggerWidth()
        {
            // Более показать не могу
        }

        private void ChangeSlowMoveTriggerHeight()
        {
            // Более показать не могу
        }

        private void ChangeSlowMoveTriggerWidth()
        {
            // Более показать не могу
        }
    }
}
#endif 