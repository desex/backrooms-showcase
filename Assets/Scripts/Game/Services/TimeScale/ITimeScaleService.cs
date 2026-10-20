using DG.Tweening;

namespace Game.Services.TimeScale
{
    public interface ITimeScaleService
    {
        void ChangeTimeScaleSmoothly(float targetTimeScale, float duration, Ease ease);
        void ChangeTimeScaleInstantly(float targetTimeScale, float targetVerticalSpeed);
        void ResetTimeScaleSmoothly(float duration, Ease ease);
        void ResetTimeScaleInstantly();
        Sequence CreateTimeScaleTween(float targetTimeScale, float duration, Ease ease);
    }
}