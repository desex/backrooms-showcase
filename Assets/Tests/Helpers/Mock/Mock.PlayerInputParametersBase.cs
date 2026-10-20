using Db.Player;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class PlayerInputParametersBase : IPlayerInputParametersBase
        {
            public float VerticalToHorizontalSwipeRatio { get; set; } = 0.1f;
            public float JoystickNormalizedRadius { get; set; } = 0.2f;
            public float JoystickNormalizedDeadZone { get; set; } = 0.05f;
        }
    }
}