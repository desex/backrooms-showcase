using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.UI.Combo.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(ComboViewParameters), fileName = nameof(ComboViewParameters))]
    public class ComboViewParameters : ScriptableObject
    {
        [field: Title("Smoke parameters")]
        [field: SerializeField] public Color SmokeColorGreen { get; private set; }
        [field: SerializeField] public Color SmokeColorGray { get; private set; }
        
        [field: Space]
        [field: Title("Container position parameters")]
        [field: SerializeField] public float ComboContainerOpenPosition { get; private set; }
        [field: SerializeField] public float ComboContainerClosePosition { get; private set; }

        [field: Space]
        [field: Title("Combo basic parameters")]
        [field: SerializeField] public float ComboAppearDuration { get; private set; } = 0.25f;
        [field: SerializeField] public Ease ComboAppearEase { get; private set; } = Ease.OutBack;
        [field: SerializeField] public float ComboDisappearDuration { get; private set; } = 0.25f;
        [field: SerializeField] public Ease ComboDisappearEase { get; private set; } = Ease.InBack;

        [field: Space]
        [field: Title("Combo disappear smoke parameters")]
        [field: SerializeField] public float ComboDisappearSmokeDuration { get; private set; } = 0.1f;
        [field: SerializeField] public Ease ComboDisappearSmokeEase { get; private set; } = Ease.OutCubic;
        
        [field: Space]
        [field: Title("Combo disappear text parameters")]
        [field: SerializeField] public Color ComboDisappearTextColor { get; private set; }
        [field: SerializeField] public float ComboDisappearTextDuration { get; private set; } = 0.25f;
        [field: SerializeField] public Ease ComboDisappearTextEase { get; private set; } = Ease.OutCubic;

        [field: Space]
        [field: Title("Combo disappear shake parameters")]
        [field: SerializeField] public float ComboDisappearShakeDuration { get; private set; } = 0.2f;
        [field: SerializeField] public float ComboDisappearShakeStrength { get; private set; } = 25f;
        [field: SerializeField] public int ComboDisappearShakeVibrato { get; private set; } = 75;

        [field: Space]
        [field: Title("Water bonus parameters")]
        [field: SerializeField] public float WaterBonusDuration { get; private set; } = 0.8f;
        [field: SerializeField] public float DelayAlmondWaterForCombo { get; private set; } = 0.15f;

        [field: Space]
        [field: Title("Combo text fade parameters")]
        [field: SerializeField] public float ComboTextInFadeDuration { get; private set; } = 0.15f;
        [field: SerializeField] public Ease ComboTextInFadeEase { get; private set; } = Ease.OutCubic;
        [field: SerializeField] public Ease ComboTextOutFadeEase { get; private set; } = Ease.InCubic;

        [field: Space]
        [field: Title("Combo text scale parameters")]
        [field: SerializeField] public float ComboTextInScaleDuration { get; private set; } = 0.3f;
        [field: SerializeField] public Ease ComboTextInScaleEase { get; private set; } = Ease.OutBack;
        [field: SerializeField] public Ease ComboTextOutScaleEase { get; private set; } = Ease.InBack;
        
        [field: Space]
        [field: Title("Combo reappear parameters")]
        [field: SerializeField] public float ComboReappearScaleValue { get; private set; }
        [field: SerializeField] public float ComboReappearInScaleDuration { get; private set; }
        [field: SerializeField] public Ease ComboReappearInScaleEase { get; private set; }
        [field: SerializeField] public float ComboReappearOutScaleDuration { get; private set; }
        [field: SerializeField] public Ease ComboReappearOutScaleEase { get; private set; }
    }
}