using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerDeathParametersBase), fileName = nameof(PlayerDeathParametersBase))]
    public class PlayerDeathParametersBase : ScriptableObject, IPlayerDeathParametersBase
    {
        [field: SerializeField] public float VerticalVelocityAfterDeath { get; private set; } = -0.175f;
        [field: SerializeField] public float HideObstaclesRadius { get; private set; } = 10f;
    }
}