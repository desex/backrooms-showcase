namespace Scheduler.Services.EcsTimerSequence
{
    public class EcsTimerSequenceDelayItem : AEcsTimerSequenceItem
    {
        public EcsTimerSequenceDelayItem(
            EEcsTimerSequenceItemType itemType,
            System.Action executeTarget,
            float delay
        ) : base(itemType, executeTarget, delay)
        {
        }
    }
}