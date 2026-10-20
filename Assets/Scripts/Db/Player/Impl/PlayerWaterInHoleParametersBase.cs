using DG.Tweening;
using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(
        menuName = "Settings/" + nameof(PlayerWaterInHoleParametersBase),
        fileName = nameof(PlayerWaterInHoleParametersBase)
    )]
    public class PlayerWaterInHoleParametersBase : ScriptableObject, IPlayerWaterInHoleParametersBase
    {
        [field: Space]
        [field: SerializeField] public float InWaterForwardSpeed { get; private set; } = 2f;

        [field: SerializeField] public float InWaterForwardSpeedDeceleration { get; private set; } = 2f;

        [field: Space]
        [field: SerializeField] public float InWaterAnimationSpeedMultiplier { get; private set; } = 0.1f;

        [field: Space]
        [field: Header("Gravity")]
        [field: SerializeField] public float FastWaterEnterVerticalSpeed { get; private set; } = -0.4f;
        [field: SerializeField] public float FastWaterEnterDuration { get; private set; } = 0.2f;
        [field: SerializeField] public Ease FastWaterEnterEase { get; private set; } = Ease.InCubic;
        [field: SerializeField] public float StayInWaterVerticalSpeed { get; private set; } = -0.02f;
        [field: SerializeField] public float SlowdownAfterEnterDuration { get; private set; } = 0.3f;
        [field: SerializeField] public Ease SlowdownAfterEnterEase { get; private set; } = Ease.OutCubic;
    }
}