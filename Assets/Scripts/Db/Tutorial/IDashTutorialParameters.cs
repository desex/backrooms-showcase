using DG.Tweening;

namespace Db.Tutorial
{
    public interface IDashTutorialParameters
    {
        public Ease AppearEase { get; }
        public float AppearDuration { get; }
        
        public float DelayBetweenSteps { get; }
        public float FadeDuration { get; }
        public float TapDuration { get; }
        public Ease FingerDownScaleEase { get; }
        public Ease FingerUpScaleEase { get; }
        public Ease DisappearEase { get; }
        public float DisappearDuration { get; }
    }
}