#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct WallGenerationPresetsGroup
        {
            public string PresetGroupName;
        }
    }
}
#endif