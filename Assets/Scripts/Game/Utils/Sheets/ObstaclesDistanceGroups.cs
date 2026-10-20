using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct ObstaclesDistanceGroups
        {
            public int GroupDistance;
            public string GroupObstaclesFilter;
        }
    }
}