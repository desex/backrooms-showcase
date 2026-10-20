namespace Db.Player
{
    public interface IPlayerHealthParametersBase
    {
        float TimeBetweenInjuries { get; }
        float TimeBeforeRemoveDamageVisuals { get; }
        float TimeBetweenDamages { get; }
        int PlayerMaxHealth { get; }
        float HitObstacleSideAngleThreshold { get; }
    }
}