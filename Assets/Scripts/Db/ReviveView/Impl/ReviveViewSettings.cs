using UnityEngine;

namespace Db.ReviveView.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(ReviveViewSettings), fileName = nameof(ReviveViewSettings))]
    public class ReviveViewSettings : ScriptableObject, IReviveViewSettings
    {
        [field: Space]
        [field: Header("BottlePerRevive")]
        [field: SerializeField]
        public float ScalePulseBottle { get; private set; } = 1.165f;
        
        [field: SerializeField]
        public float ScalePulseBottleDuration { get; private set; } = 0.55f;
        
        [field: SerializeField]
        public float FirstPulseDelay { get; private set; } = 0.5f;
        
        [field: SerializeField]
        public float SecondPulseDelay { get; private set; } = 1.8f;
        
        [field: SerializeField]
        public int CountBottlesPerAds { get; private set; } = 3;
        
        [field: SerializeField]
        public float DurationForAddBottlesAfterAds { get; private set; } = 1.5f;
        
        [field: Space]
        [field: Header("Count seconds")]
        [field: SerializeField]
        public int CounterSecond { get; private set; }
        
        [field: Space]
        [field: Header("DelayShowView")]
        [field: SerializeField]
        public float DelayFromSeconds { get; private set; } = 5;
    }
}