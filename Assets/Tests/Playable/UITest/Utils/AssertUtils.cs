using System;
using System.Collections;
using Tests.Playable.UITest.Conditions;

namespace Tests.Playable.UITest.Utils
{
    public static class AssertUtils
    {
        public static IEnumerator WaitFor(Func<bool> condition, float timeout)
        {
            var boolCondition = new BoolCondition(condition);
            var waitCondition = new WaitCondition(boolCondition, timeout);
            yield return waitCondition.GetEnumerator();
        }
    }
}