namespace Game.Services.CheckPoints
{
    public interface ICheckpointService
    {
        bool ContainsUiDistance(int distance);
        bool TrySpawn(float from, float to);
    }
}