using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Db.GenerationSettings;
using Game.Services.Providers;
using Game.Utils;
using UnityEngine;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class GenerationResourcesProvider : IGenerationResourcesProvider
        {
            public IChunkGenerationSettings ChunkGenerationSettings { get; set; }
            public IObstaclesBase ObstaclesBase { get; set; }
            public ISequenceGenerationSettings SequenceGenerationSettings { get; set; }

            public GenerationResourcesProvider()
            {
                ChunkGenerationSettings = Resources.Load<Db.GameLevelsSettings.Impls.Core.GenerationSettings.ChunkGenerationSettings>("DisabledGenerationSettings");
                ObstaclesBase = Resources.Load<ObstaclesBase>("ObstaclesBase_Pool_Default");
            }
            
            public GenerationResourcesProvider(Db.GameLevelsSettings.Impls.Core.GenerationSettings.ChunkGenerationSettings chunkGenerationSettings)
            {
                ChunkGenerationSettings = chunkGenerationSettings;
                ObstaclesBase = Resources.Load<ObstaclesBase>("ObstaclesBase_Pool_Default");
            }

            public void SetBySceneName(EGameLevelType levelType)
            {
            }
        }
    }
}
