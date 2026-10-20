namespace Db.LampHiders
{
    public interface ILampHiderModuleSettings
    {
        float StraightAngle { get; }
        float InvertedStraightAngle { get; }
        float CenterY { get; }
        float SquareY { get; }
        float AdditionalSize { get; }
    }
}