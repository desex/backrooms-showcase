using Db.GenerationSettings;
using Db.Lights;

namespace Db.GameLevelsSettings
{
    public interface IGameLevelResources
    {
        IChunkGenerationSettings ChunkGenerationSettings { get; }
        ISequenceGenerationSettings SequenceGenerationSettings { get; }
        IObstaclesBase ObstaclesBase { get; }
        ILightSettings GlobalIlluminationSettings { get; }
    }
}