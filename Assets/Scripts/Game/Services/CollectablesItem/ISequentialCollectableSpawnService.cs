using Db.GameLevelsSettings.Impls.Sequence;
using Ecs.Views.Linkable.Impl.Chunk;

namespace Game.Services.CollectablesItem
{
    public interface ISequentialCollectableSpawnService
    {
        public void SpawnCollectables(SequenceParameters targetSequence, ChunkView newChunkView);
    }
}