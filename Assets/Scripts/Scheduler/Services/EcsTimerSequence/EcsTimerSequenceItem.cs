namespace Scheduler.Services.EcsTimerSequence
{
    public abstract class AEcsTimerSequenceItem
    {
        public readonly float Delay;
        public readonly EEcsTimerSequenceItemType ItemType;
        public float GlobalDelay { get; set; }

        private readonly System.Action _executeTarget;

        public AEcsTimerSequenceItem(EEcsTimerSequenceItemType itemType, System.Action executeTarget, float delay)
        {
            ItemType = itemType;
            _executeTarget = executeTarget;
            Delay = delay;
        }

        public void Execute()
        {
            _executeTarget?.Invoke();
        }

        public override string ToString()
        {
            return $"{GlobalDelay:00.0} - {Delay:00.0}";
        }
    }
}