namespace Db.Effects
{
    public interface IAccelerationFeedbackParameters
    {
        float AppearDuration { get; }
        float DisappearDuration { get; }
        float MaxAlpha { get; }
        float MinAlpha { get; }
    }
}