using UnityEngine;

namespace Db.Player
{
    public interface IPlayerMonkeyJumpParametersBase
    {
        float MonkeyJumpCharacterRadius { get; }
        float MonkeyJumpCharacterHeight { get; }
        Vector3 MonkeyJumpCharacterCenter { get; }
        float AnimationDuration { get; }
    }
}