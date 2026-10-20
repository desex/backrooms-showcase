namespace Db.ReviveView
{
    public interface IReviveViewSettings
    {
        public int CountBottlesPerAds { get; }
        public float DurationForAddBottlesAfterAds { get; }
        public int CounterSecond { get; }
        public float DelayFromSeconds { get; }
    }
}