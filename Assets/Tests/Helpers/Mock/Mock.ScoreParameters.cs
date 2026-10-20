using System.Collections.Generic;
using Db.Score;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ScoreParameters : IScoreParameters
        {
            public List<string> DefaultWordsList = new()
            {
                "great",
                "perfect",
                "well done",
                "cool",
                "keep it up",
            };

            public List<string> ColumnWordsList = new() {"Too close"};

            public float DefaultMultiplier { get; } = 1;
            public float ComboDivideForAlmondWater { get; } = 5;
            public int ScoreForNormalActionComplete { get; set; } = 1;
            public int ScoreForFastActionComplete { get; set; } = 2;
            public int ScoreForOnePassedUnit { get; set; } = 0;
            public int SuccessActionsCountBeforeStartMultiplier { get; set; } = 2;
            public float IncreaseMultiplierValue { get; set; } = 1;
            public float StartMultiplierValue { get; set; } = 2;
            public float ResetMultiplierDelaySeconds { get; set; } = 6;
            public float DistanceToMetersRatio { get; set; } = 0.5f;
            public IReadOnlyList<string> DefaultWords => DefaultWordsList;
            public IReadOnlyList<string> ColumnWords => ColumnWordsList;
            public int SuccessActionsCountBeforeStartCombo { get; set; } = 2;
        }
    }
}