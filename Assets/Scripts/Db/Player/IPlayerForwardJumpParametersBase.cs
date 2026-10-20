using UnityEngine;

namespace Db.Player
{
    public interface IPlayerForwardJumpParametersBase
    {
        float Speed { get; }
        float CharacterRadius { get; }
        float CharacterHeight { get; }
        Vector3 CharacterCenter { get; }
        float AnimationDuration { get; }
        AnimationCurve MoveYAnimationCurve { get; }
    }
}