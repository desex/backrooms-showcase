namespace Game.Utils.MovementBlock
{
    public static class BlockedMovementExtension
    {
        public static bool IsMovementBlocked(this EBlockedMovement blockedMovement, EBlockedMovement targetMovement)
        {
            return (blockedMovement & targetMovement) != 0;
        }
    }
}