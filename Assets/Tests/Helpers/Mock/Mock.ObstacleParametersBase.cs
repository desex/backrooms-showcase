using Db.Obstacles;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ObstacleParametersBase : IObstacleParametersBase
        {
            public float ReactivationDelay { get; set; } = 1f;
            public float ReadyTriggerOffset { get; set; } = 0f;
            public float ReadyTriggerLength { get; set; } = 1f;
            public float FastMoveTriggerOffset { get; set; } = 0f;
            public float FastMoveTriggerLength { get; set; } = 3.5f;
        }
    }
}