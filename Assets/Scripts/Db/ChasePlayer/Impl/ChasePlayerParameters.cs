using DG.Tweening;
using UnityEngine;

namespace Db.ChasePlayer.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ChasePlayerParameters), fileName = nameof(ChasePlayerParameters))]
    public class ChasePlayerParameters : ScriptableObject, IChasePlayerParameters
    {
        [field: SerializeField] public float ReviveDelay { get; private set; } = 0.3f;
        [field: SerializeField] public float MonsterShowedDelay { get; private set; } = 0.2f;
        [field: SerializeField] public float DeadBlackoutDelay { get; private set; } = 0.5f;
        [field: SerializeField] public float TimeToInterpolateProgressBar { get; private set; }
        [field: SerializeField] public Ease MoveEase { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; } = 100f;

        [field: SerializeField] public float StartDistance { get; private set; } = 100f;
        [field: SerializeField] public float StartChaserSpeed { get; private set; } = 9.1f;

        [field: Range(.1f, .99f)]
        [field: Tooltip("Path that chaser will move while player is injured")]
        [field: SerializeField]
        public float PlayerInjuredBarProgression { get; private set; } = .33f;

        [field: Range(0.01f, 3f)]
        [field: SerializeField]
        public float SingleSegmentDuration { get; private set; } = 0.2f;        
        
        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float GlitchEffectThreshold { get; private set; } = 0.5f;

        [field: Space]
        [field: SerializeField] public float AfterQteDistance { get; private set; } = 1f;
        [field: SerializeField] public float AfterQteLeftTime { get; private set; } = 1f;
    }
}