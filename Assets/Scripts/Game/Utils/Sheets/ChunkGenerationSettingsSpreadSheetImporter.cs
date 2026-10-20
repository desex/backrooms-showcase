#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Utils.Chunks;
using Game.Utils.Obstacles;
using NorskaLib.Spreadsheets;

namespace Game.Utils.Sheets
{
    public sealed partial class
        ChunkGenerationSettingsSpreadSheetImporter : ASpreadSheetImporter<ChunkGenerationSettings,
        IImportableChunkGenerationSettings>
    {
        private readonly Dictionary<EChunkType, Func<IImportableChunkGenerationSettings, SerializedChunkTypeInfo>>
            _nonGenerativeChunksSettings =
                new Dictionary<EChunkType, Func<IImportableChunkGenerationSettings, SerializedChunkTypeInfo>>
                {
                    [EChunkType.TurnLeft] = x => x.TurnLeftChunk,
                    [EChunkType.TurnRight] = x => x.TurnRightChunk,
                    [EChunkType.TurnBothSides] = x => x.TurnBothSidesChunk,
                    [EChunkType.Down] = x => x.DownChunk,
                    [EChunkType.DiagonalRight] = x => x.RightDiagonalChunk,
                    [EChunkType.Dark] = x => x.DarkChunk,
                    [EChunkType.StraightColumn] = x => x.StraightColumnChunk,
                    [EChunkType.CrossColumn] = x => x.CrossColumnChunk,
                    [EChunkType.Curved] = x => x.CurvedChunk,
                    [EChunkType.Rotated] = x => x.RotatedChunk,
                    [EChunkType.TurnLeftBody] = x => x.TurnLeftBodyChunk,
                    [EChunkType.TurnRightBody] = x => x.TurnRightBodyChunk,
                    [EChunkType.TurnBothSidesBody] = x => x.TurnBothSidesBodyChunk,
                    [EChunkType.Transition] = x => x.TransitionChunk,
                    [EChunkType.TransitionHole] = x => x.TransitionHoleChunk,
                    [EChunkType.TransitionTruck] = x => x.TransitionTruckChunk,
                    [EChunkType.TransitionTrain] = x => x.TransitionTrainChunk
                };

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("ChunksWeights")]
        private List<ChunkWeight> _chunkWeights;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("ChunkDistanceGroups")]
        private List<ChunkDistanceGroups> _distanceGroups;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("GeneralChunkSettings")]
        private List<GeneralChunkSettings> _generalSettings;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("ObstaclesDistanceGroups")]
        private List<ObstaclesDistanceGroups> _obstaclesDistanceGroups;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("ObstaclesWeights")]
        private List<ObstacleWeight> _obstacleWeights;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("WallGenerationPresets")]
        private List<WallGenerationPresets> _presets;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("WallGenerationPresetsRestrictions")]
        private List<WallGenerationPresetsRestrictions> _restrictions;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("ChunksSettings")]
        private List<ChunksSettings> _settings;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("StraightChunkPresets")]
        private List<StraightChunkPresets> _straightChunkPresets;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("WallGenerationPresetsGroups")]
        private List<WallGenerationPresetsGroup> _wallGroupsPresetsGroups;

        // ReSharper disable once CollectionNeverUpdated.Local
        [SpreadsheetPage("WallGenerationPresetsGroupsWeights")]
        private List<WallGenerationPresetsGroupWeights> _wallGroupsPresetsGroupWeights;

        protected override void ParseData()
        {
            // Более показать не могу
        }

        private void ParseGeneralSettings()
        {
            // Более показать не могу
        }

        private void ParseDistanceGroups()
        {
            // Более показать не могу
        }

        private void ParseObstaclesDistanceGroups()
        {
            // Более показать не могу
        }

        private void ParseChunkSettings()
        {
            // Более показать не могу
        }

        private void ParseChunksWeights()
        {
            // Более показать не могу
        }

        private void ParseObstaclesWeights()
        {
            // Более показать не могу
        }

        private void ParseStraightChunkPresets()
        {
            // Более показать не могу
        }

        private void ParseWallGenerationPresetsGroups()
        {
            // Более показать не могу
        }

        private void ParseWallGenerationPresets()
        {
            // Более показать не могу
        }

        private void ParseWallGenerationPresetsRestrictions()
        {
            // Более показать не могу
        }
    }
}
#endif