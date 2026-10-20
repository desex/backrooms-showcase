using System.Collections.Generic;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Chunks;
using Game.Utils.Obstacles;
using Game.Utils.Sequence;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class ChunkGenerationSettings : IChunkGenerationSettings
        {
            public Vector3 InitialChunkOffset { get; set; } = new Vector3(0, 0, -50);
            public bool UseGeneration { get; set; } = false;
        
            public EChunkType LevelStartChunkType { get; set; } = EChunkType.Straight;
            public int ChunkSequenceLength { get; } = 100;
            public float DistanceToFarthestChunk { get; } = 50f;
            public Dictionary<SequenceParameters, List<SequenceParameters>> SequenceConnections;

            public Dictionary<EChunkType, ChunkTypeInfo> ChunksInfoDictionary = new Dictionary<EChunkType, ChunkTypeInfo>();
            public IReadOnlyDictionary<EChunkType, ChunkTypeInfo> ChunksInfo => ChunksInfoDictionary;
            public IReadOnlyDictionary<EChunkType, IReadOnlyList<ChunkView>> Chunks { get; }

            public List<DistanceGroup> DistanceGroupsList = new List<DistanceGroup>();
            public List<SequenceDistanceGroup> SequenceDistanceGroup = new List<SequenceDistanceGroup>();
            public IReadOnlyList<DistanceGroup> DistanceGroups => DistanceGroupsList;
            public IReadOnlyDictionary<SequenceParameters, List<SequenceParameters>> SequenceConnectionsMap => SequenceConnections;

            public IReadOnlyList<SequenceDistanceGroup> SequenceDistanceGroupList => SequenceDistanceGroup;
            public IReadOnlyList<(int distance, ObstacleDistanceGroup obstacleDistanceGroups)> ObstacleDistanceGroupsList { get; }

            public Dictionary<string, PresetRules> WallPresetsDictionary = new Dictionary<string, PresetRules>();
            public IReadOnlyDictionary<string, PresetRules> WallPresets => WallPresetsDictionary;
            
            public Dictionary<string, WallPresetGroup> WallPresetGroupsDictionary = new Dictionary<string, WallPresetGroup>();
            public IReadOnlyDictionary<string, WallPresetGroup> WallPresetGroups => WallPresetGroupsDictionary;
            
            public ObstaclesRules DistanceToObstaclesRules => new();

            public Dictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> WallPartsDictionary;
            public IReadOnlyDictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> WallParts => WallPartsDictionary;

            public List<GameObject> LampsPrefabsList = new List<GameObject>();
            public IReadOnlyList<GameObject> LampsPrefabs => LampsPrefabsList;

            public Dictionary<EObstacleType, ObstacleGroupView[]> ObstaclesDictionary;
            public IReadOnlyDictionary<EObstacleType, ObstacleGroupView[]> Obstacles => ObstaclesDictionary;
        }
    }
}