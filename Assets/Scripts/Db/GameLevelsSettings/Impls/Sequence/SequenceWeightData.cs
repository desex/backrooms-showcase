using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SequenceWeightData
    {
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Sequence", ValueLabel = "Weight")]
        public Dictionary<SequenceParametersData, int> SequencesWeights;
    }
}