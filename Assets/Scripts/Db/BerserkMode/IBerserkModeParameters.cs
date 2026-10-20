namespace Db.BerserkMode
{
    public interface IBerserkModeParameters
    {
        float ForceTouchTimeout { get; }
        float Duration { get; }
        float DefaultFov { get; }
        float BerserkModeFov { get; }
        float DefaultSpeedMultiplier { get; }
        float BerserkModeSpeedMultiplier { get; }
        int AlmondWaterPrice { get; }
        int AlmondWaterPerDelay { get; }
        int AlmondWaterSpendDelay { get; }
    }
}