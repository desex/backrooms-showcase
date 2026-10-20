using Db.GameLevelsSettings;
using Db.GenerationSettings;

namespace Game.Services.Providers.impl.GenerationSettingsProviders
{
    public class GenerationResourcesProvider : IGenerationResourcesProvider, ILevelResourcesProviderSetter
    {
        public IChunkGenerationSettings ChunkGenerationSettings { get; private set; }
        public IObstaclesBase ObstaclesBase { get; private set; }
        public ISequenceGenerationSettings SequenceGenerationSettings { get; private set; }

        public void Set(IGameLevelResources resources)
        {
            // Более показать не могу
        }
    }
}