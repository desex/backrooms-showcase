using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;
using Game.Utils.Sequence;

namespace Db.GenerationSettings
{
    public interface ISequenceGenerationSettings
    {
        SequenceParameters FirstSequenceParameters { get; }
        IReadOnlyDictionary<SequenceParametersData, SequenceWeightData> SequenceConnectionsMap { get; }
        IReadOnlyList<SequenceDistanceGroup> SequenceDistanceGroups { get; }
    }
}