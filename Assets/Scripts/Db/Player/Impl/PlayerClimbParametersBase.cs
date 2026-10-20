using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerClimbParametersBase), fileName = nameof(PlayerClimbParametersBase))]
    public class PlayerClimbParametersBase : ScriptableObject, IPlayerClimbParametersBase
    {
        [field: SerializeField] public float AnimationDuration { get; private set; }
    }
}