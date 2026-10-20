using DG.Tweening;

namespace Db.QTE
{
    public interface IQteParameters
    {
        int TapsToEscape { get; }
        float TimeForQte { get; }
        float CircleAnimationTime { get; }
        Ease FillEasing { get; }
        float TextAnimationTime { get; }
        Ease TextAnimationEase { get; }
        float BackwardHideObstaclesRadius { get; }
        float BackwardHideObstaclesPositionOffset { get; }
        
        float ForwardHideObstaclesRadius { get; }
        float ForwardHideObstaclesPositionOffset { get; }
        int MaxQteCount { get; }
        float TimeDecreasePerSuccess  { get; }
        float MaxCorrectHeight { get; }

        float BlackoutDelay { get; }
        float BlackoutFadeTime { get; }
        Ease BlackoutFadeEasing { get; }
    }
}