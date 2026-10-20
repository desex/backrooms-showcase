using Db.GameLevelsSettings.Impls.Sequence;
using Game.Utils.Chunks;

namespace Game.Services.Chunks
{
    public interface ISequentialChunkPredictionService
    {
        SequenceParameters ChooseSequence(ChunkNode node);
        SequenceParameters ChooseSequenceForSecondAnchor(GameEntity chunkEntity);
        SequenceParameters ChooseTransitionSequence(GameEntity chunkEntity);
        SequenceParameters ChoosePostTransitionSequence(GameEntity chunkEntity);
    }
}