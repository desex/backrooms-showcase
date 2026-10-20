using DG.Tweening;

namespace Db.Monsters
{
    public interface IChaserPresentationParameters
    {
        float LookingBackAnimationDelay { get; }
        float ShowedMonsterDelay { get; }
        float AfterTutorialShowedMonsterDelay { get; }
        float MoveDuration { get; }
        float StartDistance { get; }
        float MinDistance { get; }
        float TargetDistance { get; }
        Ease MoveEase { get; }
        float MoveToPlayerDelay { get; }
        float DefaultAnimationSpeed { get; }
        float DashAnimationSpeed { get; }
    }
}