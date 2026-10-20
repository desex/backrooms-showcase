using DG.Tweening;
using UnityEngine;

namespace Db.TimeScale.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SlowingDownReviveParameters), fileName = nameof(SlowingDownReviveParameters))]
    public class SlowingDownReviveParameters : ScriptableObject, ISlowingDownReviveParameters
    {
        [field: SerializeField] public float TimeScaleInterpolateEnable { get; private set; }
        [field: SerializeField] public float TimeScaleInterpolateDisable { get; private set; }
        [field: SerializeField] public float ForceGravityModifier { get; private set; }
        [field: SerializeField] public float TimeReachGravityTarget { get; private set; }
        [field: SerializeField] public Ease SlowdownEase { get; private set; }
        [field: SerializeField] public Ease DisableSlowdownEase { get; private set; }
    }
}