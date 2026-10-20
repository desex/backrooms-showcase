namespace Game.Services.Revive
{
    public interface IReviveObstacleService
    {
        bool CanReviveFromHoleObstacle(GameEntity player);
        bool CanDashFromHoleObstacle(GameEntity player);
        void UpdateReviveDataOnPlayer(GameEntity player, GameEntity obstacle);
    }
}