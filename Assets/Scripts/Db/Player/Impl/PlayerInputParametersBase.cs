using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerInputParametersBase), fileName = nameof(PlayerInputParametersBase))]
    public class PlayerInputParametersBase : ScriptableObject, IPlayerInputParametersBase
    {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float VerticalToHorizontalSwipeRatio { get; private set; } = 0.1f;

        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float JoystickNormalizedRadius { get; private set; } = 0.2f;

        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float JoystickNormalizedDeadZone { get; private set; } = 0.05f;
    }
}