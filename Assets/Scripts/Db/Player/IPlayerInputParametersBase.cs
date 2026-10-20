namespace Db.Player
{
    public interface IPlayerInputParametersBase
    {
        float VerticalToHorizontalSwipeRatio { get; }
        float JoystickNormalizedRadius { get; }
        float JoystickNormalizedDeadZone { get; }
    }
}