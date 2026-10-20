using System;
using System.Collections.Generic;
using Game.Utils.Revive;
using UnityEngine;

namespace Ecs.Utils.Revive
{
    [Serializable]
    public struct ReviveData
    {
        public EReviveType ReviveType;
        public List<ReviveMovePointData> MovePoints;
        public Transform ReviveTransform;
    }
}