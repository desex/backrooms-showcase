using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerHealthParametersBase), fileName = nameof(PlayerHealthParametersBase))]
    public class PlayerHealthParametersBase : ScriptableObject, IPlayerHealthParametersBase
    {
        [field: SerializeField] public float TimeBetweenInjuries { get; private set; }
        [field: SerializeField] public float TimeBeforeRemoveDamageVisuals { get; private set; } = 3f;
        [field: SerializeField] public float TimeBetweenDamages { get; private set;}
        [field: SerializeField] public int PlayerMaxHealth { get; private set;}
        [field: SerializeField] public float HitObstacleSideAngleThreshold { get; private set; } = 45f;
    }
}