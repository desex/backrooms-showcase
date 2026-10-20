using Game.Utils.Chunks;
using UnityEngine;

namespace Db.Ghost
{
    public interface IGhostsParameters
    {
        int GhostCount { get; }
        float StartForwardDeltaFromPlayer { get; }
        float RespawnDelay { get; }
        float CooldownAfterFailRespawn { get; }
        Vector2 ForwardReviveDeltaRange { get; }
        float TurnChunksForwardReviveOffset { get; }
        Vector2 SidesReviveDeltaRange { get; }
        float KillGhostDistance { get; }
        Vector2 ObstacleFastMoveChance { get; }
        Vector2 ObstacleFailChance { get; }
        Vector2 JumpFailChance { get; }
        float FailChancesDistance { get; }
        float GhostWidth { get; }
        float GhostHeight { get; }
        Vector2 TurningFailChance { get; }
        Vector2 ComebackFailChance { get; }
        float SuccessMovementDecreaseChanceDelta { get; }
        float FailureMovementIncreaseChanceDelta { get; }
        float StopTiltingThreshold { get; }
        Vector2 RandomTiltPointSidesDeltaRange { get; }
        Vector2 RandomTiltPointForwardDeltaRange { get; }
        EChunkType RandomTiltRestrictedChunks { get; }
        Vector2 TurnDistanceRange { get; }
        Vector2 RandomTiltMissPointSidesDeltaRange { get; }
        Vector2 RandomTiltMissPointForwardDeltaRange { get; }
        AnimationCurve RandomTiltVelocityRatioToPassedDistanceCurve { get; }
        AnimationCurve AvoidObstacleVelocityRatioToPassedDistanceCurve { get; }
    }
}