namespace Db.Player
{
    public interface IPlayerDeathParametersBase
    {
        float VerticalVelocityAfterDeath { get; }
        float HideObstaclesRadius { get; }
    }
}