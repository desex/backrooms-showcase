using DG.Tweening;
using UnityEngine;

namespace Db.Obstacles.Impls
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(BreakBoxParameters), fileName = nameof(BreakBoxParameters))]
    public class BreakBoxParameters : ScriptableObject, IBreakBoxParameters
    {
        [field: Header("Parameters for box move")]
        [field: SerializeField] public float BaseForce { get; private set; } = 10f;
        [field: SerializeField] public float ForceMultiplier { get; private set; } = 1.5f;
        [field: SerializeField] public float NormalSpeedForceBonus { get; private set; } = 1f;
        [field: SerializeField] public float VerticalBoost { get; private set; }
        [field: SerializeField] public float TimerBoxPhysicsOff { get; private set; } = 4f;
        
        [field: Header("Parameters for box speed animation"), Space]
        [field: SerializeField] public float AnimationSpeedMin { get; private set; } = 0.3f;
        [field: SerializeField] public float AnimationSpeedMax { get; private set; } = 0.8f;

        [field: Header("Parameters for box move in dash"), Space]
        [field: SerializeField] public float PlayerSpeedMin { get; private set; } = 0.1f;
        [field: SerializeField] public float PlayerSpeedMax { get; private set; } = 18f;
        [field: SerializeField] public float PlayerDashSpeedMax { get; private set; } = 80f;
        [field: SerializeField] public float DashForceMin { get; private set; } = 5f;
        [field: SerializeField] public float DashForceMax { get; private set; } = 12f;
        [field: SerializeField] public float EndVelocityMultiplier { get; private set; } = 0.12f;
        [field: SerializeField] public Ease EasingEndDash { get; private set; }
        
        [field: Header("Parameters for moving boxes to the sides relative to the player"), Space]
        [field: SerializeField] public float DistanceFlightThresholdMin { get; private set; } = 1.5f;
        [field: SerializeField] public float DistanceFlightThresholdMax { get; private set; } = 3f;
        [field: SerializeField] public float SlowingMultiplier { get; private set; } = 2f;
        
        [field: Header("Parameters for moving animation boxes"), Space]
        [field: SerializeField] public float AnimationBoxOffsetMin { get; private set; }  = 1f;
        [field: SerializeField] public float AnimationBoxOffsetMax { get; private set; }  = 6f;
        [field: SerializeField] public float AnimationBoxOffsetMinInDash { get; private set; }  = 8f;
        [field: SerializeField] public float AnimationBoxOffsetMaxInDash { get; private set; }  = 16f;
        [field: SerializeField] public float AnimationBoxMoveTimer { get; private set; }  = 0.3f;
        [field: SerializeField] public Ease EasingAnimationBox { get; private set; }
    }
}