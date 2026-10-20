using Db.GameLevelsSettings.Impls.Sequence;

namespace Game.Utils.Tutorial
{
    public readonly struct TutorialSequenceData
    {
        public readonly SequenceParameters Parameters;
        public readonly TutorialChunkData ChunkData;

        public TutorialSequenceData(
            SequenceParameters parameters, 
            TutorialChunkData chunkData
        )
        {
            Parameters = parameters;
            ChunkData = chunkData;
        }
    }
}