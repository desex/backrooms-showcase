using DG.Tweening;
using UnityEngine;

namespace Ecs.Utils.ObstaclePass
{
    public readonly struct DashData
    {
        public readonly float DashDuration;
        public readonly Vector3 PossibleStartDashPosition;
        public readonly Vector3 DashEndPosition;
        public readonly Ease DashEase;
        public readonly float ComboMoveMultiplier;
        public readonly Vector3 CenterStartReadyTriggerPosition;

        public DashData(
            float dashDuration, 
            Vector3 possibleStartDashPosition, 
            Vector3 dashEndPosition, 
            Ease dashEase, 
            float comboMoveMultiplier, 
            Vector3 centerStartReadyTriggerPosition
        )
        {
            DashDuration = dashDuration;
            PossibleStartDashPosition = possibleStartDashPosition;
            DashEndPosition = dashEndPosition;
            DashEase = dashEase;
            ComboMoveMultiplier = comboMoveMultiplier;
            CenterStartReadyTriggerPosition = centerStartReadyTriggerPosition;
        }
    }
}