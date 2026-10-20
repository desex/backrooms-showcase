namespace Game.Utils.Movement
{
    public readonly struct ForwardSpeed
    {
        public readonly float Target;
        public readonly float Current;

        public ForwardSpeed(float target, float current)
        {
            Target = target;
            Current = current;
        }
    }
}