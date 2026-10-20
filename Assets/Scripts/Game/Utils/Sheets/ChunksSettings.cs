#if UNITY_EDITOR
using System;

namespace Game.Utils.Sheets
{
    public partial class ChunkGenerationSettingsSpreadSheetImporter
    {
        [Serializable]
        private struct ChunksSettings
        {
            public string ChunkType;
            public int ChunkSequenceMaxCount;
            public string NextChunkTypes;
            public string AvailableObstacles;
            public int MinObstacleCount;
            public int MaxObstacleCount;
        }
    }
}
#endif