namespace Game.Services.Sound.Chaser
{
    public interface IChaserSoundZoneService
    {
        void HandlePlayConditions(GameEntity chaser, float chaserProgress);
    }
}