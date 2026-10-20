#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct ChunkWeight
        {
            public string ChunkType;
            public string NextChunkType;
            public int Weight;
        }
    }
}
#endif