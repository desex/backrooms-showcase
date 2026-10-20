using System.Collections.Generic;
using Game.Utils;
using UnityEngine;

namespace Db.TransitionChunk
{
    public interface ITransitionChunkParameters
    {
        float DistanceBetweenChunks { get; }
        int ChunkToGenerateAfterTransitionCount { get; }
        float UnscaledDistanceToRunBeforeTimeStop { get; }
        float SlowdownMovementDuration { get; }
        float ApplyDecreasedTimeScaleDuration { get; }
        float SlowdownTimeScale { get; }

        IReadOnlyDictionary<EGameLevelType, List<EGameLevelType>> LevelConnections { get; }
        IReadOnlyDictionary<EGameLevelType, Cubemap> LevelCubemaps { get; }
    }
}