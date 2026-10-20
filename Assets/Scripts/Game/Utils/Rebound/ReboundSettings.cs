using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Rebound
{
    [Serializable]
    public class ReboundSettings
    {
        public List<Collider> Colliders;

        [Header("Horizontal rebound:")] public EReboundDirection Direction = EReboundDirection.Left | EReboundDirection.Right;

        public EReboundForce Force = EReboundForce.Light;
        public EReboundAngle Angle = EReboundAngle.Common;

        [Header("Down chunk rebound:")] public bool EnableDownChunkRebound;

        [ShowIf(nameof(EnableDownChunkRebound))] public EReboundForce DownChunkForce = EReboundForce.Extreme;
    }
}