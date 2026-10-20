using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerWallMotionParametersBase), fileName = nameof(PlayerWallMotionParametersBase))]
    public class PlayerWallMotionParametersBase : ScriptableObject, IPlayerWallMotionParametersBase
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float RunAcceleration { get; private set; }
        [field: SerializeField] public float DurationPerUnit { get; private set; }
        [field: SerializeField] public AnimationCurve VelocityCurve { get; private set; }
        [field: SerializeField] public float VelocityModifier { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float DelayAfterJump { get; private set; }
    }
}