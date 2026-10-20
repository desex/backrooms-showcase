using DG.Tweening;

namespace Db.Obstacles.DestructibleObstacles
{
    public interface IDestructibleObstaclesParameters
    {
        float TargetAcceleration { get; }
        float AccelerationDuration { get; }
        Ease AccelerationEase { get; }
    }
}