using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerJumpOnParametersBase), fileName = nameof(PlayerJumpOnParametersBase))]
    public class PlayerJumpOnParametersBase : ScriptableObject, IPlayerJumpOnParametersBase
    {
        [field: SerializeField] public float JumpDuration { get; private set; }
        [field: SerializeField] public AnimationCurve JumpCurve { get; private set; }
    }
}