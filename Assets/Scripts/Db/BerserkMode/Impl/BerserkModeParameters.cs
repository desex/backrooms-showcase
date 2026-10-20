using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.BerserkMode.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(BerserkModeParameters), fileName = nameof(BerserkModeParameters))]
    public class BerserkModeParameters : ScriptableObject, IBerserkModeParameters
    {
        [field: SerializeField] public float ForceTouchTimeout { get; private set; } = 0.5f;
        [field: SerializeField] public float Duration { get; private set; } = 0.5f;
        [field: SerializeField] public float DefaultFov { get; private set; } = 100;
        [field: SerializeField] public float BerserkModeFov { get; private set; } = 130;
        [field: SerializeField] public float DefaultSpeedMultiplier { get; private set; } = 1f;
        [field: SerializeField] public float BerserkModeSpeedMultiplier { get; private set; } = 1.5f;
        
        [field: Title("Almond Water")]
        [field: SerializeField] public int AlmondWaterPrice { get; private set; } = 1;
        [field: SerializeField] public int AlmondWaterPerDelay { get; private set; } = 1;
        [field: SerializeField] public int AlmondWaterSpendDelay { get; private set; } = 2;
    }
}