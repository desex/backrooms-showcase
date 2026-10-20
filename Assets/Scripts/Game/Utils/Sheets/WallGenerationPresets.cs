#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct WallGenerationPresets
        {
            public string PresetName;
            public bool CanSpawnObstacles;
            public string WallType;
        }
    }
}
#endif