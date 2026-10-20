using Db.GameLevelsSettings;
using Db.GenerationSettings;

namespace Game.Services.Providers
{
    public interface IGenerationResourcesProvider
    {
        IChunkGenerationSettings ChunkGenerationSettings { get; }
        IObstaclesBase ObstaclesBase { get; }
        ISequenceGenerationSettings SequenceGenerationSettings { get; }
    }
}