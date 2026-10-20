using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Utils.Revive
{
    [Serializable]
    public struct ReviveMovePointData
    {
        public Ease Ease;
        public Transform Point;
        public float Duration;
    }
}