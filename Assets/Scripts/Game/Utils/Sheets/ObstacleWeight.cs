using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct ObstacleWeight
        {
            public string ChunkType;
            public string Obstacle;
            public int Weight;
        }
    }
}