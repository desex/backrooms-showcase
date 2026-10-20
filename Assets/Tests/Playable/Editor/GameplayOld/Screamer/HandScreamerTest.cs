using System.Collections;
using Db.GameLevelsSettings;
using Db.GameLevelsSettings.Impls.Core.GenerationSettings;
using Game.Services.Providers;
using Tests.Helpers.Mock;
using Tests.Playable.UITest;
using Tests.Playable.Utils;
using UnityEngine;
using ZenjectUtil.Test.Extensions;

namespace Tests.Playable.GameplayOld.Screamer
{
    public class HandScreamerTest : UiTest
    {
        protected override void SubstituteCommonResources()
        {
        }

        protected override void SubstituteResources()
        {
            var chunkGenerationSettings = Resources.Load<ChunkGenerationSettings>("ChunkGenerationSettingsDeadBetweenWallHandScreamer");
            ResourceSubstitute.AddSubstitute<IChunkGenerationSettings>(chunkGenerationSettings);
            
            var generationProvider = new Mock.GenerationResourcesProvider
            {
                ChunkGenerationSettings = chunkGenerationSettings,
            };
            ResourceSubstitute.AddSubstitute<IGenerationResourcesProvider>(generationProvider);
        }
        
        //[UnityTest]
        public IEnumerator PlayerDead_BetweenWallsWithHandScreamer()
        {
            yield return GameplayUtils.Load();

            yield return new WaitForSeconds(2f);

            yield return GameplayUtils.StartSmoothSwipe(Vector2.left, 8f, 20f);

            yield return new WaitForSeconds(1f);

            yield return PlayerAssert.IsDead(2f);
            
            yield return new WaitForSeconds(2f);
        }
    }
}