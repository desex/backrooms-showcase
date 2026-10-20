using DG.Tweening;
using UnityEngine;

namespace Db.Tutorial.Impl
{
    [CreateAssetMenu(menuName = "Settings/Tutorial/" + nameof(DashTutorialParametersBase), fileName = nameof(DashTutorialParametersBase))]
    public class DashTutorialParametersBase : ScriptableObject, IDashTutorialParameters
    {
        [field: Header("Params")]
        [field: SerializeField]
        public Ease AppearEase { get; private set; } = Ease.OutCubic;

        [field: SerializeField]
        public float AppearDuration { get; private set; } = 0.3f;

        [field: SerializeField]
        public float DelayBetweenSteps { get; private set; } = 0.05f;

        [field: SerializeField]
        public float FadeDuration { get; private set; } = 0.15f;

        [field: SerializeField]
        public float TapDuration { get; private set; } = 0.1f;

        [field: SerializeField]
        public Ease FingerDownScaleEase { get; private set; } = Ease.InQuad;

        [field: SerializeField]
        public Ease FingerUpScaleEase { get; private set; } = Ease.OutBack;

        [field: SerializeField]
        public Ease DisappearEase { get; private set; } = Ease.InCubic;

        [field: SerializeField]
        public float DisappearDuration { get; private set; } = 0.3f;
    }
}