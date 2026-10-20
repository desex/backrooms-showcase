using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.GenerationSettings;
using Game.Utils.Sequence;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class SequenceGenerationSettings : ISequenceGenerationSettings
        {
            public Dictionary<SequenceParametersData, SequenceWeightData> SequenceConnectionsMapDictionary = new();
            public List<SequenceDistanceGroup> SequenceDistanceGroupList = new();
            public SequenceParameters _firstSequenceParameters = new();

            public SequenceParameters FirstSequenceParameters => _firstSequenceParameters;

            public IReadOnlyDictionary<SequenceParametersData, SequenceWeightData> SequenceConnectionsMap => SequenceConnectionsMapDictionary;
            public IReadOnlyList<SequenceDistanceGroup> SequenceDistanceGroups => SequenceDistanceGroupList;
        }
    }
}