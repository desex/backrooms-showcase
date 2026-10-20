using System;
using System.Collections;
using UnityEngine;

namespace Tests.Playable.UITest.Conditions
{
    public class ContinuousConditionChecking : IEnumerable
    {
        private readonly ICondition _condition;
        private readonly float _duration;
        private float _passedTime;

        public ContinuousConditionChecking(ICondition condition, float duration)
        {
            _condition = condition;
            _duration = duration;
        }

        public IEnumerator GetEnumerator()
        {
            while (_passedTime <= _duration)
            {
                if (!_condition.Satisfied())
                    throw new Exception($"Continuous check failed ({_duration}/{_duration}s): {_condition}");

                _passedTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }
    }
}