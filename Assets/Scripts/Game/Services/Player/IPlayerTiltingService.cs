namespace Game.Services.Player
{
    public interface IPlayerTiltingService
    {
        void SetActiveTiltingCamera(GameEntity player, float direction);
        void CancelTilting(GameEntity player);
    }
}