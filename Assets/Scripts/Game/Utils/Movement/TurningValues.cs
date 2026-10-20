namespace Game.Utils.Movement
{
    public readonly struct TurningValues
    {
        public readonly float TargetAngle;
        public readonly float CurrentTurningTime;
        public readonly bool IsRightTurning;
        public readonly bool IsCameraTilting;

        public TurningValues(float targetAngle, float currentTurningTime, bool isRightTurning, bool isCameraTilting)
        {
            TargetAngle = targetAngle;
            CurrentTurningTime = currentTurningTime;
            IsRightTurning = isRightTurning;
            IsCameraTilting = isCameraTilting;
        }
    }
}