using System;
using UnityEngine;

namespace Core.Utils.TriggerCollision
{
    public struct BoxActionData
    {
        public int Uid;
        public string Name;
        public Box Box;
        public Vector3 GlobalPosition;
        public Quaternion GlobalRotation;
        public Action EnterAction;
        public Action ExitAction;

        public BoxActionData(in BoxAction boxAction, in BoxData boxData)
        {
            Uid = boxAction.Uid;
            Name = boxAction.Name;
            Box = boxData.Box;
            GlobalPosition = boxData.Center;
            GlobalRotation = boxData.Rotation;
            EnterAction = boxAction.EnterAction;
            ExitAction = boxAction.ExitAction;
        }

        private bool Equals(BoxActionData other)
        {
            return Uid == other.Uid;
        }

        public override bool Equals(object obj)
        {
            return obj is BoxActionData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Uid;
        }
    }
}