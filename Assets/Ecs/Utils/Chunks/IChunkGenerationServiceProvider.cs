using Game.Services.Chunks;

namespace Ecs.Utils.Chunks
{
    public interface IChunkGenerationServiceProvider
    {
        public IChunkGenerationService ChunkGenerationService { get; }

        public void TryChangeService(bool isFirstChunk = false);
    }
}