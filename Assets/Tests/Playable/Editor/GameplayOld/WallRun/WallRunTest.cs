using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Services.Providers;
using Tests.Helpers.Mock;
using Tests.Playable.UITest;
using Tests.Playable.UITest.Utils;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.WallRun
{
    public class WallRunTest : UiTest
    {
        private GameContext _gameContext;
        
        protected override void SubstituteCommonResources()
        {
        }

        private void Construct()
        {
            _gameContext = DiUtils.Container.Resolve<GameContext>();
        }

        protected override void SubstituteResources()
        {
            var generationProvider = new Mock.GenerationResourcesProvider();
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>("ChunkGenerationSettingsTests");
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);

            generationProvider.ChunkGenerationSettings = chunkGenerationSettings;
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(generationProvider);
        }

        //[UnityTest]
        public IEnumerator ShouldNotWallRun_WithSmoothSwipe()
        {
            yield return GameplayUtils.Load();

            Construct();
            yield return new WaitForSeconds(2f);
            
            yield return GameplayUtils.StartSmoothSwipe(Vector2.left);

            _gameContext.PlayerEntity.IsDebugChaserDeath = false;
            
            for (var i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(1f);
                yield return PlayerAssert.IsNotReadyToWallRun();
            }
        }
    }
}