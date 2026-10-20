using DG.Tweening;

namespace Db.Obstacles
{
    public interface ITravelatorParameters
    {
        float SpeedAccelerationFactor{ get; }
        float SpeedAccelerationFactorDuration{ get; }
        Ease EaseAcceleration { get; }
        float SpeedDecelerationFactor { get; }
        float SpeedDecelerationFactorDuration { get; }
        Ease EaseDeceleration { get; }
        float MinSpeedAnimation { get; }
        float MaxSpeedAnimation { get; }
        float ChaserAccelerationFactorOnLongTravelator { get; }
        float ChaserAccelerationFactorOnShortTravelator { get; }
        float ChaserDecelerationFactorOnLongTravelator { get; }
        float ChaserDecelerationFactorOnShortTravelator { get; }
        float ExitTravelatorDuration { get; }
    }
}