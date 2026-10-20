using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerJumpOverObstacleParametersBase), fileName = nameof(PlayerJumpOverObstacleParametersBase))]
    public class PlayerJumpOverObstacleParametersBase : ScriptableObject, IPlayerJumpOverObstacleParametersBase
    {
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}