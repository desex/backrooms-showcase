namespace Game.Services.CheckPoints
{
    public interface ICheckpointScoreService
    {
        void CompleteNewRecord(int distance);
        bool IsNewRecord(int distance);
        bool TryConvertToKilometers(int currentDistance, out int distance);
    }
}