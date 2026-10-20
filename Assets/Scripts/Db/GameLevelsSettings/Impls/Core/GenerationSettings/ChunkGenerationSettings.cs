using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Utils.Chunks;
using Game.Utils.Sheets;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Core.GenerationSettings
{
    [CreateAssetMenu(
        menuName = "Settings/" + nameof(ChunkGenerationSettings),
        fileName = nameof(ChunkGenerationSettings)
    )]
    [SuppressMessage("ReSharper", "Odin.OdinUnknownGroupingPath")]
    public sealed class ChunkGenerationSettings : SerializedScriptableObject,
        IChunkGenerationSettings,
        IImportableChunkGenerationSettings
    {
        [SerializeField]
        [HideInInspector]
        private List<(int distance, ObstacleDistanceGroup obstacleDistanceGroups)> _obstacleDistanceGroupsList;

        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EChunkType, ChunkTypeInfo> _chunksInfo;

        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EChunkType, IReadOnlyList<ChunkView>> _chunks;

#if UNITY_EDITOR
        [Title("Wall parts")]
        [LabelText("Wall parts dictionary")]
        [OdinSerialize]
        private Dictionary<EChunkWallType, WallViewMatrix> _wallsMatrices;
#endif

        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> _wallParts;

        [Title("Lights prefabs")]
        [SerializeField]
        private List<GameObject> _lampsPrefabs;

        [field: SerializeField]
        public bool UseGeneration { get; set; }

        [field: SerializeField]
        public Vector3 InitialChunkOffset { get; private set; } = new Vector3(0, 0, -50f);

        [field: SerializeField]
        [field: ValidateInput("ValidateStartChunkType")]
        public EChunkType LevelStartChunkType { get; set; }

        [field: SerializeField]
        [field: ValidateInput("ValidateChunkSequenceLength")]
        public int ChunkSequenceLength { get; set; }

        [field: SerializeField]
        public float DistanceToFarthestChunk { get; private set; } = 50f;

        public IReadOnlyDictionary<EChunkType, ChunkTypeInfo> ChunksInfo => _chunksInfo;
        public IReadOnlyDictionary<EChunkType, IReadOnlyList<ChunkView>> Chunks => _chunks;
        public IReadOnlyList<DistanceGroup> DistanceGroups => DistanceGroupsList;

        public IReadOnlyList<(int distance, ObstacleDistanceGroup obstacleDistanceGroups)> ObstacleDistanceGroupsList
            => _obstacleDistanceGroupsList;

        public IReadOnlyDictionary<string, PresetRules> WallPresets => WallPresetsDictionary;
        public IReadOnlyDictionary<string, WallPresetGroup> WallPresetGroups => WallPresetGroupsDictionary;
        public ObstaclesRules DistanceToObstaclesRules => ObstaclesRules;
        public IReadOnlyDictionary<EChunkWallType, IReadOnlyList<ChunkWallView>> WallParts => _wallParts;
        public IReadOnlyList<GameObject> LampsPrefabs => _lampsPrefabs;

        [field: SerializeField]
        public List<DistanceGroup> DistanceGroupsList { get; set; }

#if UNITY_EDITOR
        [field: OdinSerialize]
        public SortedDictionary<int, ObstacleDistanceGroup> ObstacleDistanceGroups { get; set; }
#endif

        [field: SerializeField]
        public ObstaclesRules ObstaclesRules { get; set; }

        [field: Title("Wall generation presets")]
        [field: OdinSerialize]
        public Dictionary<string, WallPresetGroup> WallPresetGroupsDictionary { get; set; }

        [field: OdinSerialize]
        public Dictionary<string, PresetRules> WallPresetsDictionary { get; set; }

#if UNITY_EDITOR
        [field: FoldoutGroup("Straight chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedGeneratedChunkTypeInfo StraightChunk { get; private set; } =
            new SerializedGeneratedChunkTypeInfo();

        [field: FoldoutGroup("Turn left chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnLeftChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Turn right chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnRightChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Turn both sides chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnBothSidesChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Down chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo DownChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Right diagonal chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo RightDiagonalChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Dark chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo DarkChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Straight column chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo StraightColumnChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Cross column chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo CrossColumnChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Curved chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo CurvedChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Rotated chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo RotatedChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Turn left body chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnLeftBodyChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Turn right body chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnRightBodyChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Turn both sides body chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TurnBothSidesBodyChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Transition Chunk")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TransitionChunk { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Tutorial Left Chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TutorialLeftChunks { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Tutorial Left Chunks V2")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TutorialLeftChunksV2 { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Tutorial Right Chunks")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TutorialRightChunks { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Tutorial Right Chunks V2")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TutorialRightChunksV2 { get; private set; } = new SerializedChunkTypeInfo();

        [field: FoldoutGroup("Transition hole")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TransitionHoleChunk { get; private set; } = new SerializedChunkTypeInfo();
        
        [field: FoldoutGroup("Transition truck")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TransitionTruckChunk { get; private set; } = new SerializedChunkTypeInfo();
        
        [field: FoldoutGroup("Transition train")]
        [field: OdinSerialize]
        [field: HideLabel]
        [field: HideReferenceObjectPicker]
        public SerializedChunkTypeInfo TransitionTrainChunk { get; private set; } = new SerializedChunkTypeInfo();
#endif

#if UNITY_EDITOR
        [SerializeField]
        private string _documentId;

        [Button(ButtonSizes.Gigantic)]
        private async void ImportDataFromGoogleSpreadsheets()
        {
            var importer = new ChunkGenerationSettingsSpreadSheetImporter();
            await importer.ImportData(this, _documentId);
        }

        [Button(ButtonSizes.Large)]
        private void OpenSpreadsheetInBrowser()
        {
            Process.Start($"https://docs.google.com/spreadsheets/d/{_documentId}");
        }
#endif

#if UNITY_EDITOR
        private void OnValidate()
        {
            _chunksInfo = new()
            {
                [EChunkType.Straight] = StraightChunk,
                [EChunkType.TurnLeft] = TurnLeftChunk,
                [EChunkType.TurnRight] = TurnRightChunk,
                [EChunkType.TurnBothSides] = TurnBothSidesChunk,
                [EChunkType.Down] = DownChunk,
                [EChunkType.DiagonalRight] = RightDiagonalChunk,
                [EChunkType.Dark] = DarkChunk,
                [EChunkType.StraightColumn] = StraightColumnChunk,
                [EChunkType.CrossColumn] = CrossColumnChunk,
                [EChunkType.Curved] = CurvedChunk,
                [EChunkType.Rotated] = RotatedChunk,
                [EChunkType.TurnLeftBody] = TurnLeftBodyChunk,
                [EChunkType.TurnRightBody] = TurnRightBodyChunk,
                [EChunkType.TurnBothSidesBody] = TurnBothSidesBodyChunk,
                [EChunkType.Transition] = TransitionChunk,
                [EChunkType.TutorialTurnLeftV1] = TutorialLeftChunks,
                [EChunkType.TutorialTurnRightV1] = TutorialRightChunks,
                [EChunkType.TutorialTurnLeftV2] = TutorialLeftChunksV2,
                [EChunkType.TutorialTurnRightV2] = TutorialRightChunksV2,
                [EChunkType.TransitionHole] = TransitionHoleChunk,
                [EChunkType.TransitionTruck] = TransitionTruckChunk,
                [EChunkType.TransitionTrain] = TransitionTrainChunk,
            };

            _chunks = new();

            foreach (var pair in _chunksInfo)
                _chunks[pair.Key] = pair.Value;

            _wallParts = new();

            foreach (var pair in _wallsMatrices)
                _wallParts[pair.Key] = pair.Value.ToArray();

            ObstacleDistanceGroups ??= new();

            _obstacleDistanceGroupsList = new List<(int, ObstacleDistanceGroup)>(ObstacleDistanceGroups.Count);

            foreach (var (distance, obstacleDistanceGroups) in ObstacleDistanceGroups)
                _obstacleDistanceGroupsList.Add((distance, obstacleDistanceGroups));
        }

        private bool ValidateStartChunkType(EChunkType chunkType)
        {
            var enumValues = Enum.GetValues(typeof(EChunkType)).Cast<EChunkType>().ToArray();

            for (var i = 0; i < enumValues.Length - 1; i++)
            {
                if (chunkType == enumValues[i])
                    return true;
            }

            return false;
        }

        private bool ValidateChunkSequenceLength(int _)
        {
            if (ChunkSequenceLength < 1)
                ChunkSequenceLength = 1;

            return true;
        }
#endif
    }
}