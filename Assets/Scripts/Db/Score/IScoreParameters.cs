using System.Collections.Generic;

namespace Db.Score
{
    public interface IScoreParameters
    {
        float DefaultMultiplier { get; }
        float ComboDivideForAlmondWater { get; }
        int ScoreForNormalActionComplete { get; }
        int ScoreForFastActionComplete { get; }
        int ScoreForOnePassedUnit { get; }
        int SuccessActionsCountBeforeStartMultiplier { get; }
        float IncreaseMultiplierValue { get; }
        float StartMultiplierValue { get; }
        float ResetMultiplierDelaySeconds { get; }
        float DistanceToMetersRatio { get; }
        IReadOnlyList<string> DefaultWords { get; }
        IReadOnlyList<string> ColumnWords { get; }
        int SuccessActionsCountBeforeStartCombo { get; }
    }
}