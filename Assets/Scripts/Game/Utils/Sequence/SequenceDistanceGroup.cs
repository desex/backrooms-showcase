using System;
using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Sequence
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SequenceDistanceGroup
    {
#if UNITY_EDITOR
        public string GroupName;
#endif
        
        [PropertySpace(10)]
        [MinMaxSlider(0, 10000, true)]
        public Vector2Int GroupLength;
        
        [PropertySpace(10)]
        [LabelText("Available Sequence Parameters")]
        [ListDrawerSettings(DraggableItems = false, DefaultExpandedState = true)]
        [SerializeField]
        private List<SequenceParametersData> _sequenceParametersData;

        private List<SequenceParameters> _sequenceParameters;
 
        public List<SequenceParameters> SequenceParameters => _sequenceParameters ??= GetAllSequenceParameters();
        
        private List<SequenceParameters> GetAllSequenceParameters()
        {
            // Более показать не могу
            return default;
        }
    }
}