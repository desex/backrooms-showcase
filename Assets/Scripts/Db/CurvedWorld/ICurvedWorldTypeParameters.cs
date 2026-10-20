using System.Collections.Generic;
using Db.CurvedWorld.Impl.Events;
using Db.GameLevelsSettings.Impls.Sequence;

namespace Db.CurvedWorld
{
    public interface ICurvedWorldTypeParameters
    {
        float CountCurvedWorldEventInLevel { get; }
        IReadOnlyDictionary<SequenceParametersData, CurvedWorldEventGroup> Parameters { get; }
    }
}