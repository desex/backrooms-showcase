using DG.Tweening;

namespace Db.ChasePlayer
{
    public interface IChasePlayerParameters
    {
        float ReviveDelay { get; }
        float MonsterShowedDelay { get; }
        float DeadBlackoutDelay { get; }
        float TimeToInterpolateProgressBar { get; }
        Ease MoveEase { get; }
        
        float MaxDistance { get; }
        float StartDistance { get; }
        float StartChaserSpeed { get; } 
        float PlayerInjuredBarProgression { get; }
        float SingleSegmentDuration { get; }
        float GlitchEffectThreshold { get; }
        
        float AfterQteDistance { get; }
        float AfterQteLeftTime { get; }
    }
}