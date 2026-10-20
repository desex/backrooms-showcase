using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using UnityEngine;

namespace Db.GameLevelsSettings
{
    public interface IChunkGenerationSettings
    {
        Vector3 InitialChunkOffset { get; }
        bool UseGeneration { get; }
        EChunkType LevelStartChunkType { get; }
        int ChunkSequenceLength { get; }
        float DistanceToFarthestChunk { get; }
        IReadOnlyDictionary<EChunkType, ChunkTypeInfo> ChunksInfo { get; }
        IReadOnlyDictionary<EChunkType, IReadOnlyList<ChunkView>> Chunks { get; }
        IReadOnlyList<DistanceGroup> DistanceGroups { get; }
        IReadOnlyList<(int distance, ObstacleDistanceGroup obstacleDistanceGroups)> ObstacleDistanceGroupsList { get; }
        IReadOnlyDictionary<string, PresetRules> WallPresets { get; }
        IReadOnlyDictionary<string, WallPresetGroup> WallPresetGroups { get; }
        ObstaclesRules DistanceToObstaclesRules { get; }
        IReadOnlyDictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> WallParts { get; }
        IReadOnlyList<GameObject> LampsPrefabs { get; }
    }
}