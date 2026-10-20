#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct StraightChunkPresets
        {
            public string PresetGroupName;
        }
    }
}
#endif