#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct WallGenerationPresetsRestrictions
        {
            public string Preset;
            public string WallType;
            public string RestrictionType;
            public string Argument;
        }
    }
}
#endif