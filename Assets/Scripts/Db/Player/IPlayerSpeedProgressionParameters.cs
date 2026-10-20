namespace Db.Player
{
    public interface IPlayerSpeedProgressionParameters
    {
        float InitialForwardSpeedMultiplier { get; }
        float ProgressionMultiply { get; }
        float MaxProgressionMultiply { get; }
        float InitialRunAnimationSpeed { get; }
        float MaxRunAnimationMultiply { get; }
        float MetersProgression { get; }
    }
}