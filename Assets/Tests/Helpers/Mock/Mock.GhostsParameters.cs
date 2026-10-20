using Db.Ghost;
using Game.Utils.Chunks;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class GhostsParameters : IGhostsParameters
        {
            public int GhostCount { get; set; } = 0;
            public float StartForwardDeltaFromPlayer { get; set; } = 4;
            public float RespawnDelay { get; set; } = 1.5f;
            public float CooldownAfterFailRespawn { get; set; } = 2f;
            public Vector2 ForwardReviveDeltaRange { get; set; } = new(20, 50);
            public float TurnChunksForwardReviveOffset { get; set; } = 2f;
            public Vector2 SidesReviveDeltaRange { get; set; } = new(0, 4);
            public float KillGhostDistance { get; set; } = 125;
            public Vector2 ObstacleFastMoveChance { get; set; } = new(0.1f, 0.6f);
            public Vector2 ObstacleFailChance { get; set; } = new(0.028f, 0.056f);
            public Vector2 JumpFailChance { get; set; } = new(0.056f, 0.112f);
            public float FailChancesDistance { get; set; } = 50;
            public float GhostWidth { get; set; } = 0.6f;
            public float GhostHeight { get; set; } = 2f;
            public Vector2 TurningFailChance { get; set; } = new(0.0175f, 0.035f);
            public Vector2 ComebackFailChance { get; set; } = new(0.112f, 0.168f);
            public float SuccessMovementDecreaseChanceDelta { get; set; } = 0.03f;
            public float FailureMovementIncreaseChanceDelta { get; set; } = 0.4f;
            public float StopTiltingThreshold { get; set; } = 0.05f;
            public Vector2 RandomTiltPointSidesDeltaRange { get; set; } = new(0, 5);
            public Vector2 RandomTiltPointForwardDeltaRange { get; set; } = new(15, 18);

            public EChunkType RandomTiltRestrictedChunks { get; set; } = EChunkType.TurnLeftBody
                                                                       & EChunkType.TurnRightBody
                                                                       & EChunkType.TurnBothSidesBody
                                                                       & EChunkType.Down
                                                                       & EChunkType.Curved
                                                                       & EChunkType.TurnLeft
                                                                       & EChunkType.TurnRight
                                                                       & EChunkType.TurnBothSides
                                                                       & EChunkType.DiagonalRight;

            public Vector2 TurnDistanceRange { get; set; } = new(0, 6f);
            public Vector2 RandomTiltMissPointSidesDeltaRange { get; set; } = new(1, 2);
            public Vector2 RandomTiltMissPointForwardDeltaRange { get; set; } = new(3, 10);

            public AnimationCurve RandomTiltVelocityRatioToPassedDistanceCurve { get; set; } =
                AnimationCurve.EaseInOut(0, 0.35f, 1, 0.35f);

            public AnimationCurve AvoidObstacleVelocityRatioToPassedDistanceCurve { get; set; } =
                AnimationCurve.EaseInOut(0, 0.35f, 1, 0.35f);
        }
    }
}