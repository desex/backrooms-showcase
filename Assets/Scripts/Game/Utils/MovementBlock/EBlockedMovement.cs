using System;

namespace Game.Utils.MovementBlock
{
    [Flags]
    public enum EBlockedMovement
    {
        None = 0,
        Tilting = 1 << 0,
        Turn = 1 << 1,
        Slide = 1 << 2,
        Jump = 1 << 3,
        DoubleJump = 1 << 4,
        Comeback = 1 << 5,
        Dash = 1 << 6,
        
        All = int.MaxValue, 
    }
}