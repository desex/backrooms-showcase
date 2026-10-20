using System;
using UnityEngine;

namespace Db.Player.Impl
{
    [Serializable]
    public class DashVo
    {
        public float DefaultDuration;
        public float DefaultSpeed;
        public float Duration;
        public float Speed;
        public float JumpSpeed;
        public float SpeedOnDoubleJump;
        public float VerticalSpeed;
        public float DefaultDashFov;
        public float TargetDashFov;
        [Space] public int AlmondWaterPrice;
    }
}