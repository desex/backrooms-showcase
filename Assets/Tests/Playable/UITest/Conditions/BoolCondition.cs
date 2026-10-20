using System;

namespace Tests.Playable.UITest.Conditions
{
    public class BoolCondition : Condition<string>
    {
        private readonly Func<bool> _getter;

        public BoolCondition(Func<bool> getter)
        {
            _getter = getter;
        }

        public override bool Satisfied()
        {
            return _getter != null && _getter();
        }

        public override string ToString()
        {
            return "BoolCondition(" + _getter + ")";
        }
    }
}