namespace Game.Utils.Movement
{
    public readonly struct WallRunParameters
    {
        public readonly float MaxDuration;
        public readonly float Accumulator;

        public WallRunParameters(float maxDuration)
        {
            MaxDuration = maxDuration;
            Accumulator = maxDuration;
        }

        public WallRunParameters(float maxDuration, float accumulator)
        {
            MaxDuration = maxDuration;
            Accumulator = accumulator;
        }
    }
}