using UnityEngine;

namespace Db.Player
{
    public interface IPlayerJumpOnParametersBase
    {
        float JumpDuration { get; }
        AnimationCurve JumpCurve { get; }
    }
}