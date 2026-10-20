using Ecs.Managers;

namespace Game.Services.Chunks
{
    public interface IChunkCurvedService
    {
        void CompleteSequence(Uid chunkId);
        void SetBendSettings(Uid chunkId);
    }
}