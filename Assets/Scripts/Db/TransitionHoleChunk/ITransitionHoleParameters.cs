using System.Collections.Generic;
using Db.GameLevelsSettings.Impls.Sequence;

namespace Db.TransitionHoleChunk
{
    public interface ITransitionHoleParameters
    {
        SequenceParametersData FirstSequence { get; }
        IReadOnlyDictionary<SequenceParametersData, int> Sequences { get; }
    }
}