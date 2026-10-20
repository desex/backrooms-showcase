namespace Db.Player
{
    public interface IPlayerDoubleTapParameters
    {
        float DoubleTapThreshold { get; }
        float DoubleTapThresholdCooldown { get; }
    }
}