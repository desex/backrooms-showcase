using DG.Tweening;
using Ecs.Managers;
using Ecs.Utils.ObstaclePass;
using UnityEngine;

namespace Game.Utils.Movement
{
    public readonly struct ObstaclePassData
    {
        public readonly bool IsDashEnabled;
        public readonly EObstaclePassType ObstaclePassType;
        public readonly EObstacleTypeForAcceleration ObstacleTypeForAcceleration;
        public readonly Vector3 CenterStartReadyTriggerPosition;
        public readonly float DashDuration;
        public readonly Vector3 DashEndPosition;
        public readonly Ease DashEase;
        public readonly float DashComboMoveMultiplier;
        public readonly float ObstaclePassComboMoveMultiplier;
        public readonly PassObstacleMovePointsData[] MovePoints;
        public readonly Uid Uid;

        public ObstaclePassData(
            bool isDashEnabled,
            EObstaclePassType obstaclePassType,
            Vector3 centerStartReadyTriggerPosition,
            float dashDuration,
            Vector3 dashEndPosition,
            Ease dashEase,
            float dashComboMoveMultiplier,
            float obstaclePassComboMoveMultiplier,
            PassObstacleMovePointsData[] movePoints,
            Uid uid,
            EObstacleTypeForAcceleration obstacleTypeForAcceleration
        )
        {
            IsDashEnabled = isDashEnabled;
            ObstaclePassType = obstaclePassType;
            CenterStartReadyTriggerPosition = centerStartReadyTriggerPosition;
            DashDuration = dashDuration;
            DashEndPosition = dashEndPosition;
            DashEase = dashEase;
            DashComboMoveMultiplier = dashComboMoveMultiplier;
            ObstaclePassComboMoveMultiplier = obstaclePassComboMoveMultiplier;
            MovePoints = movePoints;
            Uid = uid;
            ObstacleTypeForAcceleration = obstacleTypeForAcceleration;
        }
    }
}