using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerBasicMovementParametersBase), fileName = nameof(PlayerBasicMovementParametersBase))]
    public class PlayerBasicMovementParametersBase : ScriptableObject, IPlayerBasicMovementParametersBase
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float FastMoveRatio { get; private set; }
        [field: SerializeField] public float UpSurfaceSpeed { get; private set; }
        [field: SerializeField] public float MinimumSlopeDifference { get; private set; }
        [field: SerializeField] public float InjuredSpeed { get; private set; }
        [field: SerializeField] public float ForwardSpeedAcceleration { get; private set; }
        [field: SerializeField] public float GravityForce { get; private set; }
        [field: SerializeField] public float ExtraGravityForce { get; private set; }
        
        [field: Space]
        [field: SerializeField] public float JumpHorizontalSpeedRatio { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; } = 2f;
        [field: SerializeField] public float HorizontalSpeed { get; private set; }
        [field: SerializeField] public float InjuredHorizontalSpeed { get; private set; }
        [field: SerializeField] public float ReboundHorizontalSpeed { get; private set; }
        [field: SerializeField] public float ReboundHorizontalSpeedOnRamp { get; private set; }

        [field: Space]
        [field: Range(0, 1f)]
        [field: SerializeField] 
        public float ChaserPresentationSlowDownHorizontalSpeed { get; private set; } = 0.5f;
        
        [field: Title("Ground")]
        [field: SerializeField] public Vector3 GroundedSphereOffset { get; private set; }
        [field: SerializeField] public float GroundedSphereRadius { get; private set; }
    }
}