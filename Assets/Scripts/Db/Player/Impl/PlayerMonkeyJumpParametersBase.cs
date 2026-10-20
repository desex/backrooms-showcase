using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerMonkeyJumpParametersBase), 
                     fileName = nameof(PlayerMonkeyJumpParametersBase))]
    public class PlayerMonkeyJumpParametersBase : ScriptableObject, IPlayerMonkeyJumpParametersBase
    {
        [field: SerializeField] public float MonkeyJumpCharacterRadius { get; private set; }
        [field: SerializeField] public float MonkeyJumpCharacterHeight { get; private set; }
        [field: SerializeField] public Vector3 MonkeyJumpCharacterCenter { get; private set; }
        [field: SerializeField] public float AnimationDuration { get; private set; }
    }
}