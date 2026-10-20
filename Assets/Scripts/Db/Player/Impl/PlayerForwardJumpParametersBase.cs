using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerForwardJumpParametersBase), 
                     fileName = nameof(PlayerForwardJumpParametersBase))]
    public class PlayerForwardJumpParametersBase : ScriptableObject, IPlayerForwardJumpParametersBase
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float CharacterRadius { get; private set; }
        [field: SerializeField] public float CharacterHeight { get; private set; }
        [field: SerializeField] public Vector3 CharacterCenter { get; private set; }
        [field: SerializeField] public float AnimationDuration { get; private set; }
        [field: SerializeField] public AnimationCurve MoveYAnimationCurve { get; private set; }
    }
}