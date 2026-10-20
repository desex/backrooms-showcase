using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.LampHiders.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(LampHiderModuleSettings), fileName = nameof(LampHiderModuleSettings))]
    public sealed class LampHiderModuleSettings : SerializedScriptableObject, ILampHiderModuleSettings
    {
        [field: SerializeField] public float StraightAngle { get; private set; } = 90f;
        [field: SerializeField] public float InvertedStraightAngle { get; private set; } = 270f;
        [field: SerializeField] public float CenterY { get; private set; } = 3f;
        [field: SerializeField] public float SquareY { get; private set; } = 7f;
        [field: SerializeField] public float AdditionalSize { get; private set; } = 7f;
    }
}