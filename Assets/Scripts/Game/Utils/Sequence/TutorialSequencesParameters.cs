using System;
using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Sequence
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class TutorialSequencesParameters
    {
        [PropertySpace(10)]
        [LabelText("Available Sequence Parameters")]
        [ListDrawerSettings(DraggableItems = false, DefaultExpandedState = true)]
        [SerializeField]
        private List<SequenceParametersData> _sequenceParametersData;
        
        private List<SequenceParameters> _sequenceParameters;
        
        public IReadOnlyList<SequenceParameters> SequenceParameters => _sequenceParameters ??= GetAllSequenceParameters();

        private List<SequenceParameters> GetAllSequenceParameters()
        {
            // Более показать не могу
            return default;
        }
    }
}