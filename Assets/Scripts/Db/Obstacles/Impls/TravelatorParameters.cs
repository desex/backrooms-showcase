using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Obstacles.Impls
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(TravelatorParameters), fileName = nameof(TravelatorParameters))]
    public class TravelatorParameters : ScriptableObject, ITravelatorParameters
    {
        [field: Title("Acceleration Travelator")]
        [field: SerializeField] public float SpeedAccelerationFactor{ get; private set; }
        [field: SerializeField] public float SpeedAccelerationFactorDuration{ get; private set; }
        [field: SerializeField] public Ease EaseAcceleration { get; private set; }
        
        [field: Title("Deceleration Travelator")]
        [field: SerializeField] public float SpeedDecelerationFactor { get; private set; }
        [field: SerializeField] public float SpeedDecelerationFactorDuration { get; private set; }
        [field: SerializeField] public Ease EaseDeceleration { get; private set; }
        
        [field: Title("Exit Travelator")]
        [field: SerializeField] public float ExitTravelatorDuration { get; private set; }
        
        [field: Title("Speed Animation")]
        [field: SerializeField] public float MinSpeedAnimation { get; private set; }
        [field: SerializeField] public float MaxSpeedAnimation { get; private set; }
        
        [field: Title("Chaser Acceleration")]
        [field: SerializeField] public float ChaserAccelerationFactorOnLongTravelator { get; private set; }
        [field: SerializeField] public float ChaserAccelerationFactorOnShortTravelator { get; private set; }
        [field: Title("Chaser Deceleration")]
        [field: SerializeField] public float ChaserDecelerationFactorOnLongTravelator { get; private set; }
        [field: SerializeField] public float ChaserDecelerationFactorOnShortTravelator { get; private set; }
    }
}