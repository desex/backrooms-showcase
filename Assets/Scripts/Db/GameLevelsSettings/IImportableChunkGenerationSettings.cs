using System.Collections.Generic;
using Game.Utils.Chunks;

namespace Db.GameLevelsSettings
{
    public interface IImportableChunkGenerationSettings
    {
#if UNITY_EDITOR
        bool UseGeneration { set; }
        EChunkType LevelStartChunkType { set; }
        int ChunkSequenceLength { set; }
        ObstaclesRules ObstaclesRules { get; set; }

        List<DistanceGroup> DistanceGroupsList { set; }
        SortedDictionary<int, ObstacleDistanceGroup> ObstacleDistanceGroups { set; }

        SerializedGeneratedChunkTypeInfo StraightChunk { get; }
        SerializedChunkTypeInfo TurnLeftChunk { get; }
        SerializedChunkTypeInfo TurnRightChunk { get; }
        SerializedChunkTypeInfo TurnBothSidesChunk { get; }
        SerializedChunkTypeInfo DownChunk { get; }
        SerializedChunkTypeInfo RightDiagonalChunk { get; }
        SerializedChunkTypeInfo DarkChunk { get; }
        SerializedChunkTypeInfo StraightColumnChunk { get; }
        SerializedChunkTypeInfo CrossColumnChunk { get; }
        SerializedChunkTypeInfo CurvedChunk { get; }
        SerializedChunkTypeInfo RotatedChunk { get; }
        SerializedChunkTypeInfo TurnLeftBodyChunk { get; }
        SerializedChunkTypeInfo TurnRightBodyChunk { get; }
        SerializedChunkTypeInfo TurnBothSidesBodyChunk { get; }
        SerializedChunkTypeInfo TransitionChunk { get; }
        SerializedChunkTypeInfo TransitionHoleChunk { get; }
        SerializedChunkTypeInfo TransitionTruckChunk { get; }
        SerializedChunkTypeInfo TransitionTrainChunk { get; }

        Dictionary<string, PresetRules> WallPresetsDictionary { get; set; }
        Dictionary<string, WallPresetGroup> WallPresetGroupsDictionary { get; set; }
#endif
    }
}