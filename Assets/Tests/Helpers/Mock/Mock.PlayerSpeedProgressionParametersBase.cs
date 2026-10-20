using Db.Player;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class PlayerSpeedProgressionParameters : IPlayerSpeedProgressionParameters
        {
            public float InitialForwardSpeedMultiplier { get; set; } = 1f;
            public float ProgressionMultiply { get; set; } = 0.15f;
            public float MaxProgressionMultiply { get; set; } = 2f;
            public float InitialRunAnimationSpeed { get; set; } = 0.8f;
            public float MaxRunAnimationMultiply { get; set; } = 1.2f;
            public float MetersProgression { get; set; } = 10f;
        }
    }
}