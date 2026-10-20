using System;

namespace Core.Utils.TriggerCollision
{
    public struct BoxAction
    {
        public int Uid;
        public string Name;
        public Action EnterAction;
        public Action ExitAction;

        public static implicit operator BoxAction(in BoxActionData boxActionData)
        {
            var boxAction = new BoxAction
            {
                Uid = boxActionData.Uid,
                Name = boxActionData.Name,
                EnterAction = boxActionData.EnterAction,
                ExitAction = boxActionData.ExitAction,
            };

            return boxAction;
        }

        private bool Equals(BoxAction other)
        {
            return Uid == other.Uid;
        }

        public override bool Equals(object obj)
        {
            return obj is BoxAction other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Uid;
        }
    }
}