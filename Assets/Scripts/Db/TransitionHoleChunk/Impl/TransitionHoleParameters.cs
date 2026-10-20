using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.TransitionHoleChunk.Impl
{
    [CreateAssetMenu(
        menuName = "Settings/" + nameof(TransitionHoleParameters),
        fileName = nameof(TransitionHoleParameters)
    )]
    public class TransitionHoleParameters : SerializedScriptableObject, ITransitionHoleParameters
    {
        [OdinSerialize]
        private SequenceParametersData _firstSequence;
        
        [PropertySpace(10)]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Sequence", ValueLabel = "Weight")]
        private Dictionary<SequenceParametersData, int> _sequences = new Dictionary<SequenceParametersData, int>();

        public SequenceParametersData FirstSequence => _firstSequence;
        public IReadOnlyDictionary<SequenceParametersData, int> Sequences => _sequences;
    }
}