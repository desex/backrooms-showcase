using Db.ChasePlayer;
using DG.Tweening;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ChasePlayerParameters : IChasePlayerParameters
        {
            public float ReviveDelay { get; } = 0.3f;
            public float MonsterShowedDelay { get; } = 0.2f;
            public float DeadBlackoutDelay { get; } = 0.5f;
            public float TimeToInterpolateProgressBar { get; } = 1f;
            public Ease MoveEase { get; } = Ease.Linear;
            public float MaxDistance { get; } = 100f;
            public float ChaserSpeedProgression { get; } = 0.009f;
            public float StartDistance { get; } = 60f;
            public float StartChaserSpeed { get; } = 9.1f;
            public float ChaserFreezeDuration { get; } = 2f;
            public float ChaserSecondChangeDuration { get; } = 20f;
            public float PlayerInjuredBarProgression { get; } = 100f;
            public float SingleSegmentDuration { get; } = 0.2f;
            public float GlitchEffectThreshold { get; } = 0.5f;

            public float AfterQteDistance { get; } = 1f;
            public float AfterQteLeftTime { get; } = 1f;
        }
    }
}