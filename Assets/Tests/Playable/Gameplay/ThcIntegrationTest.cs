using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Db.GameLevelsSettings.Impls.Obstacles;
using Db.GameLevelsSettings.Impls.Sequence;
using Db.GenerationSettings;
using Game.Services.Providers;
using Tests.Helpers.Mock;
using Tests.Playable.UITest;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.Gameplay
{
    public abstract class AIntegrationTest : UiTest
    {
        private Mock.GenerationResourcesProvider _generationResourcesProvider;
        private ChunkGenerationSettings _chunkGenerationSettings;
        protected ObstaclesBase ObstaclesBase;
        protected SequenceGenerationSettings SequenceGenerationSettings;
        
        protected virtual string ObstaclesBasePath => "TemplateGeneration/TemplateObstaclesBase";
        protected virtual string GenerationSettingsPath => "TemplateGeneration/TemplateGenerationSettings";
        protected virtual string SequenceGenerationSettingsPath => "TemplateGeneration/TemplateSequenceGenerationSettings";
        
        protected override void SubstituteCommonResources()
        {
   //         SetObstaclesBase(ObstaclesBasePath);
            SetSequenceGenerationSettings(SequenceGenerationSettingsPath);
            SetGenerationSettings(GenerationSettingsPath);
        }

        protected void SetGenerationSettings(string chunkGenerationSettingsPath)
        {
            _chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>(chunkGenerationSettingsPath);
            ResourceSubstitute.ReplaceSubstitute<IChunkGenerationSettings>(_chunkGenerationSettings);

            _generationResourcesProvider = new Mock.GenerationResourcesProvider();
            
            _generationResourcesProvider.ChunkGenerationSettings = _chunkGenerationSettings;
            
            if (ObstaclesBase != null)
                _generationResourcesProvider.ObstaclesBase = ObstaclesBase;

            if (SequenceGenerationSettings != null)
                _generationResourcesProvider.SequenceGenerationSettings = SequenceGenerationSettings;

            ResourceSubstitute.ReplaceSubstitute<IGenerationResourcesProvider>(_generationResourcesProvider);
        }

        protected void SetObstaclesBase(string obstaclesBasePath)
        {
            ObstaclesBase = Resources.Load<ObstaclesBase>(obstaclesBasePath);
            ResourceSubstitute.ReplaceSubstitute<IObstaclesBase>(ObstaclesBase);
        }
        
        protected void SetSequenceGenerationSettings(string sequenceGenerationSettingsPath)
        {
            SequenceGenerationSettings = Resources.Load<SequenceGenerationSettings>(sequenceGenerationSettingsPath);
        }
    }
}