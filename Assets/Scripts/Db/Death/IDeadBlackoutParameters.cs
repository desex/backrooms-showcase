using DG.Tweening;

namespace Db.Death
{
    public interface IDeadBlackoutParameters
    {
        float FadeDuration { get; }
        Ease FadeEase { get; }
    }
}