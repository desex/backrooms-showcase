#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct ChunkDistanceGroups
        {
            public int GroupLength;
            public string GroupChunksFilter;
        }
    }
}
#endif