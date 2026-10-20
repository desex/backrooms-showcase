using System;
using System.Collections.Generic;
using Db.GenerationSettings;
using Game.Utils.Sequence;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [CreateAssetMenu(menuName = "Settings/Sequence/" + nameof(SequenceGenerationSettings), fileName = nameof(SequenceGenerationSettings))]
    public class SequenceGenerationSettings : SerializedScriptableObject, ISequenceGenerationSettings
    {
        [OdinSerialize] private SequenceParametersData _firstSequenceParameters; 
        
        [OdinSerialize]
        [ListDrawerSettings(DraggableItems = false, DefaultExpandedState = true)]
        private List<SequenceDistanceGroup> _sequenceDistanceGroup;
        
        [PropertySpace(10)]
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Sequence From", ValueLabel = "Sequences To")]
        private Dictionary<SequenceParametersData, SequenceWeightData> _sequenceConnections;
        
        private bool _isValidConnections;
        private bool _isValidDistanceGroups;

        public SequenceParameters FirstSequenceParameters => _firstSequenceParameters.SequenceParameters;
        public IReadOnlyDictionary<SequenceParametersData, SequenceWeightData> SequenceConnectionsMap => GetSequenceConnectionsMap();
        public IReadOnlyList<SequenceDistanceGroup> SequenceDistanceGroups => GetSequenceDistanceGroups();

        private IReadOnlyList<SequenceDistanceGroup> GetSequenceDistanceGroups()
        {
            if (_isValidDistanceGroups)
                return _sequenceDistanceGroup;
            
            foreach (var group in _sequenceDistanceGroup)
            {
                foreach (var parameter in group.SequenceParameters)
                {
                    if (parameter == null)
                        throw new NullReferenceException($"{nameof(SequenceDistanceGroup)} SequenceParameters is null in groupLength {group.GroupLength}");
                }
            }
            
            _isValidDistanceGroups = true;
            
            return _sequenceDistanceGroup;
        }
        
        private IReadOnlyDictionary<SequenceParametersData, SequenceWeightData> GetSequenceConnectionsMap()
        {
            if (_isValidConnections)
                return _sequenceConnections;
            
            foreach (var connection in _sequenceConnections)
            {
                if (connection.Key.SequenceParameters == null)
                    throw new NullReferenceException($"{nameof(SequenceGenerationSettings)} SequenceParameters is null in connection");

                foreach (var weight in connection.Value.SequencesWeights)
                {
                    if (weight.Key.SequenceParameters == null)
                        throw new NullReferenceException($"{nameof(SequenceGenerationSettings)} SequenceParameters is null in weights {weight.Key.SequenceParameters.name}");
                }
            }

            _isValidConnections = true;
            
            return _sequenceConnections;
        }
    }
}