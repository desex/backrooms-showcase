#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct GeneralChunkSettings
        {
            public bool UseGeneration;
            public string LevelStartChunk;
            public int ChunkSequenceLength;
            public int LeftSpawnBorder;
            public int RightSpawnBorder;
        }
    }
}
#endif