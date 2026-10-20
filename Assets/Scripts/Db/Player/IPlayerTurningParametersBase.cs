using System.Collections.Generic;
using Db.Player.Impl;
using Game.Utils.Chunks;
using UnityEngine;

namespace Db.Player
{
    public interface IPlayerTurningParametersBase
    {
        float EndTiltTime { get; }
        float TurningAngle { get; }
        AnimationCurve TurningCurve { get; }
        TurnDurationData DefaultTurningDuration { get; }
        Dictionary<EChunkType, TurnDurationData> ChunkDurations { get; }
    }
}