using System;
using System.Collections.Generic;
using Ecs.Scheduler.Extensions;

namespace Scheduler.Services.EcsTimerSequence
{
    public class EcsTimerSequence
    {
        private readonly SchedulerContext _scheduler;
        private readonly List<AEcsTimerSequenceItem> _sequenceItems = new();

        private EcsTimerSequence(SchedulerContext scheduler)
        {
            _scheduler = scheduler;
        }

        public static EcsTimerSequence Create()
        {
            var instance = new EcsTimerSequence(Contexts.SharedInstance.Scheduler);

            return instance;
        }

        public EcsTimerSequence Append(Action execute, float delay)
        {
            var item = new EcsTimerSequenceDelayItem(EEcsTimerSequenceItemType.Append, execute, delay);
            _sequenceItems.Add(item);

            return this;
        }

        public EcsTimerSequence AppendInterval(Action execute, float delay, float interval, float duration)
        {
            var item = new EcsTimerSequenceIntervalItem(EEcsTimerSequenceItemType.Append, execute, delay, interval,
                duration);
            _sequenceItems.Add(item);

            return this;
        }

        public EcsTimerSequence Join(Action execute, float delay)
        {
            var item = new EcsTimerSequenceDelayItem(EEcsTimerSequenceItemType.Join, execute, delay);
            _sequenceItems.Add(item);

            return this;
        }

        public void Run()
        {
            SetupGlobalDelayForItems();

            for (var i = 0; i < _sequenceItems.Count; i++)
            {
                var item = _sequenceItems[i];

                switch (item)
                {
                    case EcsTimerSequenceDelayItem:
                        _scheduler.CreateTimerAction(() =>
                        {
                            item.Execute();
                            //possible complete invoke
                        }, item.GlobalDelay);
                        break;
                    case EcsTimerSequenceIntervalItem intervalItem:

                        _scheduler.CreateTimerAction(() =>
                        {
                            _scheduler.CreateIntervalActionWithElapsed(() =>
                            {
                                item.Execute();
                                //possible complete invoke
                            }, intervalItem.Interval, intervalItem.Duration);
                        }, item.GlobalDelay);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(item));
                }
            }
        }

        private void SetupGlobalDelayForItems()
        {
            var previousDelay = 0f;

            for (var i = 0; i < _sequenceItems.Count; i++)
            {
                var sequenceItem = _sequenceItems[i];

                switch (sequenceItem.ItemType)
                {
                    case EEcsTimerSequenceItemType.Append:
                        previousDelay += sequenceItem.Delay;
                        sequenceItem.GlobalDelay = previousDelay;
                        if (sequenceItem is EcsTimerSequenceIntervalItem intervalItem)
                            previousDelay += intervalItem.Duration;
                        break;
                    case EEcsTimerSequenceItemType.Join:
                        sequenceItem.GlobalDelay = previousDelay + sequenceItem.Delay;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}