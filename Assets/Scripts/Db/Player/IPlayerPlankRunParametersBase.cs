namespace Db.Player
{
    public interface IPlayerPlankRunParametersBase
    {
        float CentralizeDuration { get; }
        float SlowDownRatioIncreaseSpeed { get; }
        float SlowDownRatioDecreaseSpeed { get; }
        float MaxSlowDownRatio { get; }
    }
}