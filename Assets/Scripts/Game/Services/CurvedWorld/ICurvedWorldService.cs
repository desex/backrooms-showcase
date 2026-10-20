using Db.GameLevelsSettings.Impls.Sequence;

namespace Game.Services.CurvedWorld
{
    public interface ICurvedWorldService
    {
        int TrySetBendSettings(out bool isSuccessfully, SequenceParameters sequenceParameters);
        void ResetCurvedWorld();
        void ResetCountCurvedWorldEventInLevel();
        void AddCountCurvedWorldEventInLevel(int count);
    }
}