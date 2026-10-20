namespace Scheduler.Services.EcsTimerSequence
{
    public class EcsTimerSequenceIntervalItem : AEcsTimerSequenceItem
    {
        public readonly float Interval;
        public readonly float Duration;

        public EcsTimerSequenceIntervalItem(
            EEcsTimerSequenceItemType itemType,
            System.Action executeTarget,
            float delay,
            float interval,
            float duration
        ) : base(itemType, executeTarget, delay)
        {
            Interval = interval;
            Duration = duration;
        }
    }
}