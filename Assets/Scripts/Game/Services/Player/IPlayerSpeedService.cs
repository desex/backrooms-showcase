namespace Game.Services.Player
{
    public interface IPlayerSpeedService
    {
        void ChangeHorizontalSensitivity(float multiply = 1f);
        float GetForwardSpeed(GameEntity entity);
        float GetHorizontalSpeed(GameEntity player);
        float GetBasicForwardSpeed(GameEntity player);
        float GetMaxTargetForwardSpeed(GameEntity player);
    }
}